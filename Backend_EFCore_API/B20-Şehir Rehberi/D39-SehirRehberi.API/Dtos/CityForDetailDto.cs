using D39_SehirRehberi.API.Models;

namespace D39_SehirRehberi.API.Dtos
{
    public class CityForDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
