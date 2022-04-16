using ServicioWebRH.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServicioWebRH
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Personal> Trabajadores();

        [OperationContract]
        List<Personal> BuscarTrabajadorxNombre(string cadena);

        [OperationContract]
        List<Agrupaciones> ListadoAgrupaciones();

        [OperationContract]
        Agrupaciones DameAgrupacionxId(int id);

        [OperationContract]
        Agrupaciones DameAgrupacionxNombre(string nombre);

        [OperationContract]
        PersonalRH DamePersonaxExp(int expediente);

        [OperationContract]
        PersonalRH DamePersonaxCi(string ci);

        [OperationContract]
        string DameNombreDepartamentoPersonaxExp(int exp);

        [OperationContract]
        List<RegistroESpejo> TrazasEnRangoxListadoExp(DateTime ini, DateTime fin, int[] expedientes);

        [OperationContract]
        string DameCatOcupacionalxExp(int expediente);

        [OperationContract]
        string DameCatOcupacionalxExpBaja(int expediente);

        [OperationContract]
        PersonalRH DamePersonaxExpDeep(int exp);

        [OperationContract]
        PersonalRH DamePersonaxCiDeep(string ci);

        [OperationContract]
        List<Personal> Cumples(DateTime inicial, DateTime final);

        [OperationContract]
        decimal CuantiaRetornar(int expediente);

        [OperationContract]
        int AñosTrabajados(int expediente);

        [OperationContract]
        string Cargo(int expediente);

        [OperationContract]
        double SalarioBase(int expediente);

        [OperationContract]
        string Municipio(int Cod_Municip, int Id_Prov, string Cod_Pais);

        [OperationContract]
        List<RegistroESpejo> Registros(int exp, DateTime fecha);

        [OperationContract]
        List<AltasBajas> AltasxAño(int año, int mes);

        [OperationContract]
        List<Bajas> BajasxAño(int año, int mes);

        [OperationContract]
        List<Categorias> Categorizacion(string tipo);

        [OperationContract]
        List<PersonalRH> DameTodosTrabajadores();

        [OperationContract]
        List<Departamentos> DameDepartamentosxAgrupacion(int idAgrupacion);

        [OperationContract]
        List<DatosInformeP4> InformeP4 ();

        [OperationContract]
        List<DatosInformeCategorizacion> InformeCategorizacion();

        [OperationContract]
        List<PersonalRH> DameTrabajadoresxDepartamento(Dictionary<short, short> plantilla);

        [OperationContract]
        Dictionary<short, short> DameTodosTrabajadoresxPlantilla(int IdAgrupacion);

    }

    [DataContract]
    public class AltasBajas
    {
        [DataMember]
        public int Exp { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Plaza { get; set; }

        [DataMember]
        public char Sexo { get; set; }

        [DataMember]
        public int Edad { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }

        [DataMember]
        public string Raza { get; set; } = "";

    }

    [DataContract]
    public class Bajas
    {
        [DataMember]
        public int Exp { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public char catOcup { get; set; }

        [DataMember]
        public char Sexo { get; set; }

        [DataMember]
        public int Edad { get; set; }

        [DataMember]
        public DateTime FechaAlta { get; set; }

        [DataMember]
        public DateTime FechaBaja { get; set; }

        [DataMember]
        public string motivo { get; set; }

    }

    [DataContract]
    public class Categorias
    {
        [DataMember]
        public string Tipo { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public int CantidadH { get; set; }

        [DataMember]
        public int CantidadM { get; set; }

        [DataMember]
        public int Total { get; set; }

        [DataMember]
        public int CantidadCDS { get; set; }

        [DataMember]
        public int CantidadCD { get; set; }

        [DataMember]
        public int CantidadCE { get; set; }        

        [DataMember]
        public int CantidadTecnico { get; set; }

    }

    [DataContract]
    public class DatosInformeP4
    {
        [DataMember]
        public int Exp { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string CarnetId { get; set; }

        [DataMember]
        public string Agrupacion { get; set; }

        [DataMember]
        public string Departamento { get; set; }

        [DataMember]
        public string Ocupacion { get; set; }

        [DataMember]
        public string Resolucion { get; set; }

        [DataMember]
        public string GrupoSalarial { get; set; }

        [DataMember]
        public float SalarioEscala { get; set; }

        [DataMember]
        public float GCientifico { get; set; }

        [DataMember]
        public float Condiciones { get; set; }

        [DataMember]
        public float Plus { get; set; }

        [DataMember]
        public float Total { get; set; }

        [DataMember]
        public string CategoriaOcupacional { get; set; }

        [DataMember]
        public string Profesion { get; set; }

        [DataMember]
        public string EstudiosTerminados { get; set; }

        [DataMember]
        public string Piel { get; set; }

        [DataMember]
        public string Sexo { get; set; }

        [DataMember]
        public int Edad { get; set; }

        [DataMember]
        public string Provincia { get; set; }

        [DataMember]
        public string Municipio { get; set; }

        [DataMember]
        public DateTime Alta { get; set; }
    }

    [DataContract]
    public class DatosInformeCategorizacion
    {
        [DataMember]
        public int Exp { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Agrupacion { get; set; }

        [DataMember]
        public string Ocupacion { get; set; }

        [DataMember]
        public string GradoCientifico { get; set; }

        [DataMember]
        public string CategoriaTecnologica { get; set; }

        [DataMember]
        public string CategoriaCientifica { get; set; }

        [DataMember]
        public string CategoriaBiotecnologica { get; set; }

        [DataMember]
        public string CategoriaDocente { get; set; }

        [DataMember]
        public string Sexo { get; set; }

        [DataMember]
        public int Edad { get; set; }

    }

}
