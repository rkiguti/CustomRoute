using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace CustomRoute.App_Start
{
    public class RestricaoPaisRouteHandler : IRouteHandler
    {
        #region Campos

        private List<string> paises;

        #endregion

        #region Construtor

        public RestricaoPaisRouteHandler(List<string> paises)
        {
            this.paises = paises;
        }

        #endregion

        #region Métodos

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            BloqueioIpHandler handler = new BloqueioIpHandler(this.paises, requestContext);

            return handler;
        }

        #endregion
    }
}