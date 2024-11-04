namespace GNC_Rodrigues.Client.Servicios
{
    public class HttpServicio
    {
        private readonly HttpClient http;

        public HttpServicio(HttpClient http)
        {
            this.http = http;
        }
    }
}
