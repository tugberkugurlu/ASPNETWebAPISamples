using AtomPubSample.AtomPub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;

namespace AtomPubSample.Formatters {
    
    public class AtomPubMediaFormatter : BufferedMediaTypeFormatter {

        private const string AtomMediaType = "application/atom+xml";

        public AtomPubMediaFormatter() {

            SupportedMediaTypes.Add(new MediaTypeHeaderValue(AtomMediaType));
            this.AddQueryStringMapping("format", "atom", AtomMediaType);
        }

        public override bool CanReadType(Type type) {

            return typeof(IPublicationCommand).IsAssignableFrom(type);
        }

        public override bool CanWriteType(Type type) {

            return typeof(IPublication).IsAssignableFrom(type)
                || typeof(IPublicationFeed).IsAssignableFrom(type)
                || typeof(IPublicationMedia).IsAssignableFrom(type);
        }

        public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger) {

            HttpContentHeaders contentHeaders = content == null ? null : content.Headers;

            // If content length is 0 then return default value for this type
            if (contentHeaders != null && contentHeaders.ContentLength == null) {

                return GetDefaultValueForType(type);
            }

            try {
                using (readStream) {
                    using (var reader = XmlReader.Create(readStream)) {

                        var formatter = new Atom10ItemFormatter();
                        formatter.ReadFrom(reader);

                        var command = Activator.CreateInstance(type);
                        ((IPublicationCommand)command).ReadSyndicationItem(formatter.Item);

                        return command;
                    }
                }
            }
            catch (Exception e) {

                if (formatterLogger == null) {
                    throw;
                }
                formatterLogger.LogError(String.Empty, e);
                return GetDefaultValueForType(type);
            }
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content) {
            using (writeStream) {
                if (value is IPublicationFeed) 
                {
                    WriteAtomFeed((IPublicationFeed)value, writeStream);
                } 
                else if(value is IPublicationMedia) 
                {
                    WriteAtomMediaEntry((IPublicationMedia)value, writeStream);
                }
                else {

                    WriteAtomEntry((IPublication)value, writeStream);
                }
            }
        }

        private void WriteAtomFeed(IPublicationFeed feed, Stream writeStream) {
            var formatter = new Atom10FeedFormatter(feed.Syndicate());

            using (var writer = XmlWriter.Create(writeStream)) {
                formatter.WriteTo(writer);
            }
        }

        private void WriteAtomEntry(IPublication publication, Stream writeStream) {
            var entry = publication.Syndicate();

            var formatter = new Atom10ItemFormatter(entry);

            using (var writer = XmlWriter.Create(writeStream)) {
                formatter.WriteTo(writer);
            }
        }

        private void WriteAtomMediaEntry(IPublicationMedia publication, Stream writeStream) {

            SyndicationItem mediaEntry = publication.Syndicate();

            var formatter = new Atom10ItemFormatter(mediaEntry);

            using (var writer = XmlWriter.Create(writeStream)) {
                formatter.WriteTo(writer);
            }
        }
    }
}