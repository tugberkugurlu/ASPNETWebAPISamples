using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HypermediaIntro.Models {
    
    public abstract class Resource {

        private readonly List<Link> links = new List<Link>();

        [JsonProperty(Order = 100)]
        public IEnumerable<Link> Links { get { return links; } }

        public void AddLink(Link link) {
            
            links.Add(link);
        }

        public void AddLinks(params Link[] links) {

            foreach (var link in Links) {

                AddLink(link);
            }
        }
    }
}