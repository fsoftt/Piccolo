using Domain.Common;
using Domain.Organizations.Errors;
using Domain.Organizations.ValueObjects;

namespace Domain.Organizations
{
    public sealed class Organization : AggregateRoot
    {
        private readonly List<OrganizationMember> members = [];

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

        public static Result<Organization> Create(
            OrganizationName name,
            Guid ownerUserId)
        {
            Organization organization = new Organization(Guid.NewGuid(), name);

            Result ownerResult = organization.AddOwner(ownerUserId);
            if (ownerResult.IsFailure)
            {
                return Result<Organization>.Failure(ownerResult.Error);
            }

            return Result<Organization>.Success(organization);
        }

        private Result AddOwner(Guid userId)
        {
            if (members.Any(x => x.Role == OrganizationRole.Owner))
            {
                return Result.Failure(
                    OrganizationErrors.OwnerAlreadyExists);
            }

            var owner = OrganizationMember.CreateOwner(
                Id,
                userId);

            members.Add(owner);

            return Result.Success();
        }

        public Result AddMember(Guid userId)
        {
            if (members.Any(x => x.UserId == userId))
            {
                return Result.Failure(
                    OrganizationErrors.DuplicateMember);
            }

            var member = OrganizationMember.CreateMember(
                Id,
                userId);

            members.Add(member);

            return Result.Success();
        }
    }
}
