using Domain.Common;
using Domain.Instruments;
using Domain.Organizations.Errors;
using Domain.Organizations.ValueObjects;

namespace Domain.Organizations
{
    public sealed class Organization : AggregateRoot
    {
        private readonly List<OrganizationMember> members = [];
        private readonly List<OrganizationInstrument> instruments = [];

        private Organization()
        {
        }

        private Organization(
            Guid id,
            OrganizationName name)
        {
            Id = id;
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public OrganizationName Name { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public IReadOnlyCollection<OrganizationMember> Members
            => members.AsReadOnly();

        public IReadOnlyCollection<OrganizationInstrument> Instruments =>
            instruments.AsReadOnly();

        public static Result<Organization> Create(
            OrganizationName name,
            Guid ownerUserId)
        {
            Organization organization = new Organization(Guid.NewGuid(), name);

            Result<OrganizationMember> ownerResult = organization.AddOwner(ownerUserId);
            if (ownerResult.IsFailure)
            {
                return Result<Organization>.Failure(ownerResult.Error);
            }

            return Result<Organization>.Success(organization);
        }

        private Result<OrganizationMember> AddOwner(Guid userId)
        {
            if (members.Any(x => x.Role == OrganizationRole.Owner))
            {
                return Result<OrganizationMember>.Failure(
                    OrganizationErrors.OwnerAlreadyExists);
            }

            var owner = OrganizationMember.CreateOwner(
                Id,
                userId);

            members.Add(owner);

            return Result<OrganizationMember>.Success(owner);
        }

        public Result<OrganizationMember> AddMember(Guid userId)
        {
            if (members.Any(x => x.UserId == userId))
            {
                return Result<OrganizationMember>.Failure(
                    OrganizationErrors.DuplicateMember);
            }

            var member = OrganizationMember.CreateMember(
                Id,
                userId);

            members.Add(member);

            return Result<OrganizationMember>.Success(member);
        }

        public Result ConfigureInstruments(
            IEnumerable<OrganizationInstrumentInfo> instruments)
        {
            ArgumentNullException.ThrowIfNull(instruments);

            if (!instruments.Any())
            {
                return Result.Failure(OrganizationErrors.AtLeastOneInstrumentRequired);
            }
            if (HasDuplicateNames(instruments))
            {
                return Result.Failure(OrganizationErrors.DuplicateInstrumentName);
            }

            var requestedInstruments = instruments.ToDictionary(
                x => x.Name.Trim(),
                StringComparer.OrdinalIgnoreCase);

            var instrumentsToRemove = this.instruments
                .Where(x => !requestedInstruments.ContainsKey(x.Name))
                .ToList();
            foreach (var instrument in instrumentsToRemove)
            {
                this.instruments.Remove(instrument);
            }

            var existingNames = this.instruments
                .Select(x => x.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
            foreach (var instrument in instruments)
            {
                if (existingNames.Contains(instrument.Name))
                {
                    continue;
                }

                this.instruments.Add(
                    new OrganizationInstrument(
                        Guid.NewGuid(),
                        Id,
                        instrument.Name,
                        instrument.Family,
                        instrument.InstrumentDefinitionId));
            }

            return Result.Success();
        }

        public Result UpdateMemberStatus(Guid userId, MemberStatus status)
        {
            var member = members.FirstOrDefault(x => x.UserId == userId);
            if (member is null)
            {
                return Result.Failure(OrganizationErrors.MemberNotFound);
            }

            if (member.Role == OrganizationRole.Owner && status != MemberStatus.Active)
            {
                return Result.Failure(OrganizationErrors.CannotDeactivateOwner);
            }

            if (member.Status == status)
            {
                return Result.Success();
            }

            member.SetStatus(status);

            return Result.Success();
        }

        private static bool HasDuplicateNames(
            IEnumerable<OrganizationInstrumentInfo> instruments)
        {
            return instruments
                .GroupBy(
                    x => x.Name.Trim(),
                    StringComparer.OrdinalIgnoreCase)
                .Any(x => x.Count() > 1);
        }

        public Result<OrganizationInstrument> AddInstrument(
            string name,
            InstrumentFamily family,
            Guid? instrumentDefinitionId)
        {
            name = name.Trim();

            var alreadyExists = instruments.Any(x =>
                string.Equals(
                    x.Name,
                    name,
                    StringComparison.OrdinalIgnoreCase));
            if (alreadyExists)
            {
                return Result<OrganizationInstrument>.Failure(
                    OrganizationErrors.DuplicateInstrumentName);
            }

            var instrument = new OrganizationInstrument(
                Guid.NewGuid(),
                Id,
                name,
                family,
                instrumentDefinitionId);

            instruments.Add(instrument);

            return Result<OrganizationInstrument>.Success(instrument);
        }

        public Result RemoveInstrument(Guid instrumentId)
        {
            var instrument = instruments.FirstOrDefault(
                x => x.Id == instrumentId);

            if (instrument is null)
            {
                return Result.Failure(
                    OrganizationErrors.InstrumentNotFound);
            }

            instruments.Remove(instrument);

            return Result.Success();
        }

        public Result UpdateInstrument(
            Guid instrumentId,
            string name,
            InstrumentFamily family,
            Guid? instrumentDefinitionId)
        {
            name = name.Trim();

            var instrument = instruments.FirstOrDefault(
                x => x.Id == instrumentId);
            if (instrument is null)
            {
                return Result.Failure(
                    OrganizationErrors.InstrumentNotFound);
            }

            var duplicateName = instruments.Any(x =>
                x.Id != instrumentId &&
                string.Equals(
                    x.Name,
                    name,
                    StringComparison.OrdinalIgnoreCase));
            if (duplicateName)
            {
                return Result.Failure(
                    OrganizationErrors.DuplicateInstrumentName);
            }

            instrument.Update(
                name,
                family,
                instrumentDefinitionId);

            return Result.Success();
        }

        public Result UpdateMemberRole(Guid userId, OrganizationRole role)
        {
            var member = members.FirstOrDefault(x => x.UserId == userId);
            if (member is null)
            {
                return Result.Failure(OrganizationErrors.MemberNotFound);
            }

            if (member.Role == role)
            {
                return Result.Success();
            }

            if (role == OrganizationRole.Owner && members.Any(x => x.Role == OrganizationRole.Owner && x.UserId != userId))
            {
                return Result.Failure(OrganizationErrors.OwnerAlreadyExists);
            }

            member.SetRole(role);

            return Result.Success();
        }

        public Result RemoveMember(Guid userId)
        {
            var member = members.FirstOrDefault(x => x.UserId == userId);
            if (member is null)
            {
                return Result.Failure(OrganizationErrors.MemberNotFound);
            }

            if (member.Role == OrganizationRole.Owner)
            {
                return Result.Failure(OrganizationErrors.CannotRemoveOwner);
            }

            members.Remove(member);

            return Result.Success();
        }
    }
}
