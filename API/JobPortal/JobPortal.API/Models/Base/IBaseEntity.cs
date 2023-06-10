namespace JobPortal.API.Models.Base
{
    public interface IBaseEntity
    {
        // Id poate fi int/guid(string de forma litere si cifre)
        Guid Id { get; set; }

        DateTime? DateCreated { get; set; }

        // ? - poate fi null
        DateTime? DateModified { get; set; }
    }
}
