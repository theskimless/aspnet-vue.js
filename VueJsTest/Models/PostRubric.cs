using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJsTest.Models
{
    public class PostRubric
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int RubricId { get; set; }
        public Rubric Rubric { get; set; }
    }
}
