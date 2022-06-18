namespace MVCMovie.Models.ViewModels
{
    public class MovieVM
    {
        public GenreMovie MovieView { get; set; }
        public Member Member { get; set; }
        public RoleStatus RoleStatus { get; set; }

        public IEnumerable<Genre> GenreList { get; set; }
    }
}
