using System.ComponentModel;

namespace Lombard.Domain.Enums
{
    public enum RegistrationStatuses
    {
        [Description("Kirayə")]
        Rent,
        [Description("Qeydiyyat üzrə")]
        Registered,
        [Description("Qohum evi")]
        RelativesHouse,
        [Description("Şəxsi mənzil")]
        PrivateApartment

    }
}
