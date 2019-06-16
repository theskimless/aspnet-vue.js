using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueJsTest.Models
{
    public class Rubric
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PostRubric> PostRubrics { get; set; }

        public Rubric()
        {
            PostRubrics = new List<PostRubric>();
        }
    }
}
