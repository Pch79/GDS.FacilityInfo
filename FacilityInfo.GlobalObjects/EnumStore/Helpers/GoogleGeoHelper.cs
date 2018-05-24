using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FacilityInfo.GlobalObjects.Helpers
{
    public static class GoogleGeoHelper
    {

        public static Dictionary<string,object> getGeoCodes(string requestData)
        {
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(requestData));
            var request = WebRequest.Create(requestUri);
            var requestAnswer = string.Empty;
            
            Dictionary<string, object> locDict = new Dictionary<string, object>();
            try
            {
                var response = request.GetResponse();

                var xdoc = XDocument.Load(response.GetResponseStream());

                var result = xdoc.Element("GeocodeResponse").Element("result");
                var resultStatus = xdoc.Element("GeocodeResponse").Element("status");
                requestAnswer = resultStatus.Value;
                Console.WriteLine("Antwortstatus: " + resultStatus.Value);
               
                var locationElement = result.Element("geometry").Element("location");
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");

                Console.WriteLine("latitude: " + lat.Value);
                Console.WriteLine("longitude: " + lng.Value);
                
                locDict.Add("latitude", lat.Value);
                locDict.Add("longitude", lng.Value);

            }
            catch (Exception geoException)
            {
                locDict.Add("Fehler", requestAnswer);
                
            }
            return locDict;

        }
    }
}
