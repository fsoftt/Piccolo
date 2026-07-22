using Domain.Instruments;

namespace Infrastructure.Persistence.Seeds
{
    public static class InstrumentDefinitionSeed
    {
        public static IReadOnlyCollection<InstrumentDefinition> Create()
        {
            return new List<InstrumentDefinition>
            {
                // Brass
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000101"), "Trumpet 1", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000102"), "Trumpet 2", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000103"), "Trumpet 3", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000104"), "Cornet", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000105"), "Flugelhorn", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000106"), "French Horn", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000107"), "Tenor Horn", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000108"), "Baritone", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000109"), "Euphonium", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00010A"), "Trombone 1", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00010B"), "Trombone 2", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00010C"), "Bass Trombone", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00010D"), "Tuba", InstrumentFamily.Brass),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00010E"), "Sousaphone", InstrumentFamily.Brass),

                // Woodwind
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000201"), "Piccolo", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000202"), "Flute", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000203"), "Oboe", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000204"), "English Horn", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000205"), "Bassoon", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000206"), "Eb Clarinet", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000207"), "Bb Clarinet 1", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000208"), "Bb Clarinet 2", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000209"), "Bb Clarinet 3", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00020A"), "Alto Clarinet", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00020B"), "Bass Clarinet", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00020C"), "Contrabass Clarinet", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00020D"), "Soprano Saxophone", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00020E"), "Alto Saxophone 1", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00020F"), "Alto Saxophone 2", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000210"), "Tenor Saxophone", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000211"), "Baritone Saxophone", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000212"), "Bass Saxophone", InstrumentFamily.Woodwind),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000213"), "Recorder", InstrumentFamily.Woodwind),

                // Strings
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000301"), "Violin I", InstrumentFamily.Strings),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000302"), "Violin II", InstrumentFamily.Strings),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000303"), "Viola", InstrumentFamily.Strings),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000304"), "Cello", InstrumentFamily.Strings),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000305"), "Double Bass", InstrumentFamily.Strings),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000306"), "Harp", InstrumentFamily.Strings),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000307"), "Acoustic Guitar", InstrumentFamily.Strings),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000308"), "Electric Guitar", InstrumentFamily.Strings),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000309"), "Electric Bass", InstrumentFamily.Strings),

                // Keyboard
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000401"), "Piano", InstrumentFamily.Keyboard),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000402"), "Organ", InstrumentFamily.Keyboard),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000403"), "Keyboard", InstrumentFamily.Keyboard),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000404"), "Synthesizer", InstrumentFamily.Keyboard),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000405"), "Celesta", InstrumentFamily.Keyboard),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000406"), "Accordion", InstrumentFamily.Keyboard),

                // Percussion (non-pitched & pitched)
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000501"), "Snare Drum", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000502"), "Bass Drum", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000503"), "Concert Bass Drum", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000504"), "Drum Set", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000505"), "Crash Cymbals", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000506"), "Suspended Cymbal", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000507"), "Hi-Hat", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000508"), "Ride Cymbal", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000509"), "Triangle", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00050A"), "Tambourine", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00050B"), "Cowbell", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00050C"), "Wood Block", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00050D"), "Claves", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00050E"), "Guiro", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00050F"), "Cabasa", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000510"), "Maracas", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000511"), "Shaker", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000512"), "Castanets", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000513"), "Timpani", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000514"), "Xylophone", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000515"), "Marimba", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000516"), "Vibraphone", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000517"), "Glockenspiel", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000518"), "Tubular Bells", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000519"), "Chimes", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00051A"), "Congas", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00051B"), "Bongos", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00051C"), "Timbales", InstrumentFamily.Percussion),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C00051D"), "Cajón", InstrumentFamily.Percussion),

                // Voice
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000601"), "Soprano", InstrumentFamily.Voice),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000602"), "Mezzo Soprano", InstrumentFamily.Voice),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000603"), "Alto", InstrumentFamily.Voice),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000604"), "Tenor", InstrumentFamily.Voice),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000605"), "Baritone", InstrumentFamily.Voice),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000606"), "Bass", InstrumentFamily.Voice),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000607"), "Choir", InstrumentFamily.Voice),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000608"), "Narrator", InstrumentFamily.Voice),

                // Other / Electronics / Misc
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000701"), "Conductor Score", InstrumentFamily.Other),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000702"), "Click Track", InstrumentFamily.Other),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000703"), "Electronics", InstrumentFamily.Other),
                new(Guid.Parse("A0D7E8A4-75A8-4E18-8E65-6A4B6C000704"), "Soloist", InstrumentFamily.Other)
            };
        }
    }
}
