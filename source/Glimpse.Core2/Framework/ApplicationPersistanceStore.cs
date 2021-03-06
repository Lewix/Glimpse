using System;
using System.Collections.Generic;
using System.Linq;
using Glimpse.Core2.Extensibility;

namespace Glimpse.Core2.Framework
{
    public class ApplicationPersistanceStore : IPersistanceStore
    {
        private IDataStore DataStore { get; set; }
        private GlimpseMetadata Metadata { get; set; }
        internal IList<GlimpseRequest> GlimpseRequests { get; set; }
        private const string PersistanceStoreKey = "__GlimpsePersistanceKey";

        public ApplicationPersistanceStore(IDataStore dataStore)
        {
            DataStore = dataStore;

            var glimpseRequests = DataStore.Get<IList<GlimpseRequest>>(PersistanceStoreKey);
            if (glimpseRequests == null)
            {
                glimpseRequests = new List<GlimpseRequest>();
                DataStore.Set(PersistanceStoreKey, glimpseRequests);
            }
            GlimpseRequests = glimpseRequests;
        }

        public void Save(GlimpseRequest request)
        {
            GlimpseRequests.Add(request);
        }

        public void Save(GlimpseMetadata metadata)
        {
            Metadata = metadata;
        }

        public GlimpseRequest GetByRequestId(Guid requestId)
        {
            return GlimpseRequests.FirstOrDefault(r => r.RequestId == requestId);
        }

        public TabResult GetByRequestIdAndTabKey(Guid requestId, string tabKey)
        {
            if (string.IsNullOrEmpty(tabKey)) throw new ArgumentException("tabKey");
            
            var request = GlimpseRequests.FirstOrDefault(r => r.RequestId == requestId);

            if (request == null || !request.PluginData.ContainsKey(tabKey))
                return null;

            return request.PluginData[tabKey];
        }

        public IEnumerable<GlimpseRequest> GetByRequestParentId(Guid parentRequestId)
        {
            return GlimpseRequests.Where(r => r.ParentRequestId == parentRequestId).ToList();
        }

        public IEnumerable<GlimpseRequest> GetTop(int count)
        {
            return GlimpseRequests.Take(count).ToList();
        }

        public GlimpseMetadata GetMetadata()
        {
            return Metadata;
        }
    }
}