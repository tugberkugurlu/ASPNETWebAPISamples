using HypermediaIntro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace HypermediaIntro {

    public class CarResponseEnricher : IResponseEnricher {

        public bool CanEnrich(HttpResponseMessage response) {

            var content = response.Content as ObjectContent;
            return content != null
                && (content.ObjectType == typeof(Car) || content.ObjectType == typeof(IEnumerable<Car>));
        }

        public HttpResponseMessage Enrich(HttpResponseMessage response) {

            Car car;
            UrlHelper urlHelper = response.RequestMessage.GetUrlHelper();
            if (response.TryGetContentValue<Car>(out car)) {
                Enrich(car, urlHelper);
            }

            IEnumerable<Car> cars;
            if (response.TryGetContentValue<IEnumerable<Car>>(out cars)) {
                foreach (var carItem in cars) {
                    Enrich(carItem, urlHelper);
                }
            }

            return response;
        }

        private void Enrich(Car car, UrlHelper url) {

            string selfUrl = url.Link(
                Constants.DefaultHttpRoute, 
                new { controller = "cars", id = car.Id });

            car.AddLink(new SelfLink(selfUrl));
        }
    }
}