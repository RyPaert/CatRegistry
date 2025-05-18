using CatRegistry.Domain;

namespace CatRegistry.Data
{
    public class DbInitializer
    {
        public static void Initialize(KittyContext context)
        {
            if(context.Kittys.Any())
            {
                return;
            }

            var kittys = new Kitty[]
            {
                new Kitty()
                {
                    KittyId = Guid.NewGuid(),
                    KittySpeciesName = "European Wildcat",
                    KittyDescription = "The European wildcat (Felis silvestris) is a small wildcat species native to continental Europe, Great Britain, Turkey and the Caucasus. \n" +
                                       "Its fur is brownish to grey with stripes on the forehead and on the sides and has a bushy tail with a black tip. \n" +
                                       "It reaches a head-to-body length of up to 65 cm (26 in) with a 34.5 cm (13.6 in) long tail, and weighs up to 7.5 kg (17 lb).",
                    KittyRegionOfOrigin = "Europe"
                }
            };
        }
    }
}
