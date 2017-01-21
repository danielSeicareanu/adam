using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Twenty.Domain;
using Twenty.Domain.Interfaces;
using Twenty.EntityTracker.Interfaces;

namespace Twenty.Web.Services.Audit.Controllers
{
    /// <summary>
    /// Web service for tracking entity properties and fields 
    /// entity can be any type but must derive from EntityTrackerBase on the web services side only
    /// Current implementation will track only properties and fields of primitive data types
    /// </summary>
    public class EntityTrackerController : ApiController
    {
        ITrackableEntity trackableService;

        // DI set up in class ServicesModule
        public EntityTrackerController(ITrackableEntity trackableService)
        {
            this.trackableService = trackableService;
        }

        // Clients of the class need to call Audit method for first time to record current properties
        // for example after entity is queried from the database
        // After those properties and fields change we call Audit method again to register current properties
        // and update previous properties (see EntityTrackerBase for implementation)
        [HttpPost]
        public HttpResponseMessage Audit(object entity)
        {
            trackableService.Audit((IAuditable)entity);
            return Request.CreateResponse(HttpStatusCode.OK, entity);
        }

        // Will return the audit update summary for public fields user might have changed
        [HttpPost]
        public HttpResponseMessage GetChangedFieldsAuditMessage(object entity)
        {
            string message = trackableService.GetChangedFieldsAuditMessage((IFieldsAuditMessage)entity);
            return Request.CreateResponse(HttpStatusCode.OK, message);
        }

        // Will return the audit update summary for public properties user might have changed
        [HttpPost]
        public HttpResponseMessage GetChangedPropertiesAuditMessage(object entity)
        {
            string message = trackableService.GetChangedPropertiesAuditMessage((IPropertiesAuditMessage)entity);
            return Request.CreateResponse(HttpStatusCode.OK, message);
        }
    }
}
