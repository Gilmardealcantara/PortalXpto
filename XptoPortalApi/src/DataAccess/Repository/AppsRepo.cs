using XptoPortalApi.DataAcess.Interfaces;
using XptoPortalApi.Models;

namespace XptoPortalApi.DataAcess.Repository

{
    public class AppsRepo : BaseRepository<App>, IAppsRepo
    {
        public AppsRepo(MainContext context) : base(context) { }
    }
}
