using Microsoft.EntityFrameworkCore;

namespace DotnetApp.Models {

    public class ProfileContext : DbContext  
    {
        public ProfileContext(DbContextOptions<ProfileContext> options)
            : base(options)
        { }

        public DbSet<Profile> Profiles { get; set; }
    }

    public class Profile {
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ThumbUrl { get; set; }
        public string CoverUrl { get; set; }
        public string City { get; set; }
        public string FlickrTags { get; set; }
        public string Description { get; set; }
    }
}