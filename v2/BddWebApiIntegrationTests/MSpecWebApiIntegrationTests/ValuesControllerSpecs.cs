using Machine.Specifications;
using Microsoft.Owin.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiApp.Core;

namespace MSpecWebApiIntegrationTests
{
    [Subject("/values")]
    public class when_get_request_sent_with_no_parameters_no_accept_header
    {
        static TestServer Server;
        static HttpResponseMessage Response;

        Establish context = () => 
        {
            Server = TestServer.Create<Startup>();
        };

        Cleanup cleanup = () => 
        {
            Server.Dispose();
        };

        Because of = () => Response = Server.CreateRequest("/values").GetAsync().Result;

        It should_have_httpstatuscode_of_200 = () => Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        It should_have_content_type_json = () => Response.Content.Headers.ContentType.MediaType.ShouldEqual("application/json");
    }

    [Subject("/values")]
    public class when_get_request_sent_with_no_parameters_xml_accept_header
    {
        static TestServer Server;
        static HttpResponseMessage Response;

        Establish context = () =>
        {
            Server = TestServer.Create<Startup>();
        };

        Cleanup cleanup = () =>
        {
            Server.Dispose();
        };

        Because of = () => Response = Server
            .CreateRequest("/values")
            .AddHeader("Accept", "application/xml")
            .GetAsync().Result;

        It should_have_httpstatuscode_of_200 = () => Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        It should_have_content_type_xml = () => Response.Content.Headers.ContentType.MediaType.ShouldEqual("application/xml");
    }

    [Subject("/values")]
    public class when_get_request_sent_with_no_parameters_json_accept_header
    {
        static TestServer Server;
        static HttpResponseMessage Response;

        Establish context = () =>
        {
            Server = TestServer.Create<Startup>();
        };

        Cleanup cleanup = () =>
        {
            Server.Dispose();
        };

        Because of = () => Response = Server
            .CreateRequest("/values")
            .AddHeader("Accept", "application/json")
            .GetAsync().Result;

        It should_have_httpstatuscode_of_200 = () => Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
        It should_have_content_type_json = () => Response.Content.Headers.ContentType.MediaType.ShouldEqual("application/json");
    }
}