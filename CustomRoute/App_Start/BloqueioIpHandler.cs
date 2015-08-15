using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;

namespace CustomRoute.App_Start
{
    public class BloqueioIpHandler : MvcHandler
    {
        #region Campos

        private List<string> paises;

        #endregion

        #region Construtor

        public BloqueioIpHandler(List<string> paises, RequestContext requestContext)
            : base(requestContext)
        {
            this.paises = paises;
        }

        #endregion

        #region Métodos

        private string ObterCodigoPais(string ip)
        {
            string query = string.Format("http://api.hostip.info/?ip={0}", ip);

            XDocument doc = XDocument.Load(query);

            XNamespace defaultNamespace = doc.Root.GetDefaultNamespace();
            XNamespace xNamespace = doc.Root.GetNamespaceOfPrefix("gml");

            string pais = doc.Root.Element(xNamespace + "featureMember").Element
                (defaultNamespace + "Hostip").Element(defaultNamespace + "countryAbbrev").Value;

            return pais;
        }

        protected override IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object state)
        {
            string pais = ObterCodigoPais(httpContext.Request.UserHostAddress);
           
            if (this.paises.Contains(pais))
                httpContext.AddError(new Exception("Desculpe! Você não pode acessar esta página a partir do seu país."));

            return base.BeginProcessRequest(httpContext, callback, state);
        }

        #endregion
    }
}