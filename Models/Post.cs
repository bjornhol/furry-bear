using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Raven.Imports.Newtonsoft.Json;

namespace FurryBear.Models
{
    public class Post
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        public DateTime CachedAt { get; set; }
    }
}