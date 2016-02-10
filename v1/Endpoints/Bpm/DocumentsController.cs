using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Endpoints.Bpm
{
    /// <summary>
    /// Document API
    /// </summary>
    [Gale.Security.Oauth.Jwt.Authorize]
    public class DocumentsController : Gale.REST.RestController
    {

        #region DOCUMENT'S
        /// <summary>
        /// List all document's
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(System.Net.HttpStatusCode.OK)]
        public IHttpActionResult Get()
        {
            return new Gale.REST.Http.HttpQueryableActionResult<Models.V_Document>(this.Request);
        }

        /// <summary>
        /// Retrieve Document Information Filtered by Type
        /// </summary>
        /// <param name="type">Document type to filter</param>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("/{type}")]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(System.Net.HttpStatusCode.OK)]
        public IHttpActionResult GetByType(String type)
        {
            //---------------------------------------------------------------------------
            // Setting Static Values
            var config = new Gale.REST.Queryable.OData.Builders.GQLConfiguration();
            config.filters.Add(new Gale.REST.Queryable.OData.Builders.GQLConfiguration.Filter()
            {
                field = "type_identifier",
                operatorAlias = "eq",
                value = type
            });
            //---------------------------------------------------------------------------

            return new Gale.REST.Http.HttpQueryableActionResult<Models.V_Document>(this.Request, config);
        }

        /// <summary>
        /// Retrieve Document Information
        /// </summary>
        /// <param name="id">Document Token</param>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("/{id:Guid}")]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(System.Net.HttpStatusCode.OK)]
        public IHttpActionResult Get(Guid id)
        {
            return new Services.Document.Get(id);
        }

        /// <summary>
        /// Retrieve Document Information
        /// </summary>
        /// <param name="identifier">Document Identifeir</param>
        /// <returns></returns>
        [HttpGet]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [HierarchicalRoute("/$filter=Name eq '{identifier}'")]
        public IHttpActionResult Get(String identifier)
        {
            return new Services.Document.GetByIdentifier(identifier);
        }

        #endregion

        #region DOCUMENT TYPES

        /// <summary>
        /// Retrieve Availables Document Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("/Types")]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(System.Net.HttpStatusCode.OK)]
        public IHttpActionResult GetDocumentTypes()
        {
            return new Gale.REST.Http.HttpQueryableActionResult<Models.V_DocumentTypes>(this.Request);
        }
        #endregion

        #region HISTORY
        /// <summary>
        /// Retrieve Document History Information
        /// </summary>
        /// <param name="id">Document Token</param>
        /// <returns></returns>
        [HttpGet]
        [HierarchicalRoute("/{id:Guid}/History")]
        [Swashbuckle.Swagger.Annotations.SwaggerResponseRemoveDefaults]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(System.Net.HttpStatusCode.OK)]
        public IHttpActionResult GetHistory(String id)
        {
            return new Services.Document.History(id);
        }
        #endregion
   
    }
}