using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel.Reponses.Prescriptions;

namespace apihealthcareconnect.ResponseMappings
{
    public class PrescriptionResponseMapping
    {
        public PrescriptionWithoutByteResponseViewModel mapPrescriptionsAppointments(Prescriptions prescription)
        {
            var response = new PrescriptionWithoutByteResponseViewModel(
                prescription.cd_prescription!.Value,
                prescription.nm_prescription_file,
                prescription.nm_prescription_extension,
                prescription.dt_prescription
            );

            return response;
        }

        public PrescriptionWithByteResponseViewModel mapPrescriptionsGeneral(Prescriptions prescription)
        {
            var response = new PrescriptionWithByteResponseViewModel(
                prescription.cd_prescription!.Value,
                prescription.nm_prescription_file,
                prescription.nm_prescription_extension,
                prescription.dt_prescription,
                prescription.prescription
            );

            return response;
        }

    }
}
