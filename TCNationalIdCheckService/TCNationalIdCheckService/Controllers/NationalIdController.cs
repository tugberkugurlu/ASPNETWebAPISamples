using System.Globalization;
using System.Threading.Tasks;
using System.Web.Http;
using TCNationalIdCheckService.NationalIdService;

namespace TCNationalIdCheckService.Controllers {

    public class NationalIdController : ApiController {

        private static readonly CultureInfo _trCulture = CultureInfo.ReadOnly(new CultureInfo("tr-TR"));

        public async Task<bool> Get(long nationalId, string name, string surname, int year) {

            // https://tckimlik.nvi.gov.tr
            // https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx
            // http://ercanbozkurt.blogspot.com/2009/09/tc-kimlik-no-dogrulama-algoritmas.html
            // Onemli NOT: Servise dogrulama icin gonderilecek ad ve soyad bilgileri tamamen buyuk harflerle yazilmis olarak gonderilmelidir.

            string upercaseName = name.ToUpper(_trCulture);
            string upercaseSurname = surname.ToUpper(_trCulture);

            KPSPublicSoapClient soapClient = new KPSPublicSoapClient();
            TCKimlikNoDogrulaResponse response = await soapClient.TCKimlikNoDogrulaAsync(nationalId, upercaseName, upercaseSurname, year);
            return response.Body.TCKimlikNoDogrulaResult;
        }
    }
}