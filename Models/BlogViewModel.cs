namespace HRP1.Models
{
    public class BlogViewModel
    {
        public int id { get; set; }

        public int postId { get; set; }
        public string? Name { get; set; }
        public string? email { get; set; }
        public Comments? comments { get; set; }

        public string? commentbody { get; set; }
        public string? Title { get; set; }

        public string? Body { get; set; }
        public int commentID { get; set; }
        public int postIdForComments { get; set; }


    }
}
