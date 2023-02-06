namespace ASassignment.Pages
{
    public class SessionService
    {
        private readonly ISession _session;

        public SessionService(IHttpContextAccessor accessor)
        {
            _session = accessor.HttpContext.Session;
        }

        public string Username
        {
            get { return _session.GetString("username"); }
            set { _session.SetString("username", value); }
        }
    }
}
