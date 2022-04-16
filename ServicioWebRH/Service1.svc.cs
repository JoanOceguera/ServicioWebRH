using FunYCon;
using ServicioWebRH.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ServicioWebRH
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        readonly RecursosHumanosEntities rhData = new RecursosHumanosEntities();
        readonly LectorHuellasEntities lhData = new LectorHuellasEntities();


        /// <summary>
        /// Devuelve una lista con todos los trabajadores del centro
        /// </summary>
        /// <returns>Devuelve una lista con todos los trabajadores del centro</returns>
        public List<Personal> Trabajadores()
        {
            try
            {
                var personal = lhData.Personal.ToList();
                foreach (var item in personal)
                {
                    item.Nombre = TConeccion.Revisar_Ort(item.Nombre);
                    item.Apellido1 = TConeccion.Revisar_Ort(item.Apellido1);
                    item.Apellido2 = TConeccion.Revisar_Ort(item.Apellido2);
                }
                return personal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Devuelve el nombre del municipio dado su identificador 
        /// </summary>
        /// <returns>Devuelve el nombre del municipio dado su identificador</returns>
        public string Municipio(int Cod_Municip, int Id_Prov, string Id_Pais)
        {
            var municipio = rhData.Municipios.Where(x => x.Id_Pais.Equals(Id_Pais) && x.Id_Prov == Id_Prov && x.Cod_Mpio == Cod_Municip).SingleOrDefault();
            if (municipio != null)
                return TConeccion.Revisar_Ort(municipio.Nomb_Mpio);
            else
                return "";
        }


        /// <summary>
        /// Busca los datos de una persona dado el exp
        /// </summary>
        /// <param name="expediente">int que responde al solapin</param>
        /// <returns>retorna un PERSONALRH con los datos de la persona que tiene ese solapín </returns>
        public PersonalRH DamePersonaxExp(int expediente)
        {
            try
            {
                var personal = rhData.Personal.Where(x => x.Exp == expediente);
                if (personal.Any())
                {
                    var persona = personal.FirstOrDefault();
                    persona.Nombre = TConeccion.Revisar_Ort(persona.Nombre);
                    persona.Apellido1 = TConeccion.Revisar_Ort(persona.Apellido1);
                    persona.Apellido2 = TConeccion.Revisar_Ort(persona.Apellido2);
                    persona.Direccion = TConeccion.Revisar_Ort(persona.Direccion);
                    return persona;
                }
                else
                {
                    throw new Exception("No existe trabajador con el solapín : " + expediente);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca los datos de una persona dado el ci
        /// </summary>
        /// <param name=ci">cadena de texto de once dígitos que responde al ci</param>
        /// <returns>retorna un PERSONALRH con los datos de la persona que tiene ese ci </returns>
        public PersonalRH DamePersonaxCi(string ci)
        {
            try
            {
                var personal = rhData.Personal.Where(x => x.CarneId.Equals(ci));

                if (personal.Any())
                {
                    foreach (var persona in personal)
                    {
                        persona.Nombre = TConeccion.Revisar_Ort(persona.Nombre);
                        persona.Apellido1 = TConeccion.Revisar_Ort(persona.Apellido1);
                        persona.Apellido2 = TConeccion.Revisar_Ort(persona.Apellido2);
                        persona.Direccion = TConeccion.Revisar_Ort(persona.Direccion);
                    }
                    return personal.FirstOrDefault();
                }
                else
                {
                    throw new Exception("No existe trabajador con este Carnet de Identidad: " + ci);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca los trabajadores que contengan en el nombre o los apellidos la cadena de texto que se pase
        /// </summary>
        /// <param name="cadena"> cadena de texto a buscar</param>
        /// <returns>devuelve una Lista de Personal con los datos de los trabajadores que coinciden con la búsqueda </returns>
        public List<Personal> BuscarTrabajadorxNombre(string cadena)
        {
            try
            {
                var personal = lhData.Personal.Where(x => (x.Nombre.ToUpper().Contains(cadena.ToUpper())) || (x.Apellido1.ToUpper().Contains(cadena.ToUpper())) || (x.Apellido2.ToUpper().Contains(cadena.ToUpper())));
                if (personal.Any())
                {
                    foreach (var item in personal)
                    {
                        item.Nombre = TConeccion.Revisar_Ort(item.Nombre);
                        item.Apellido1 = TConeccion.Revisar_Ort(item.Apellido1);
                        item.Apellido2 = TConeccion.Revisar_Ort(item.Apellido2);
                    }
                    return personal.ToList();
                }
                else
                {
                    throw new Exception("Búsqueda Fallida");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listado con todas las agrupaciones del centro 
        /// </summary>
        /// <returns>Listado de Agrupacion con todas las agrupaciones del centro </returns>
        public List<Agrupaciones> ListadoAgrupaciones()
        {
            try
            {
                var agrupaciones = rhData.Agrupaciones.ToList();
                foreach (var item in agrupaciones)
                {
                    item.Nom_Agrup = TConeccion.Revisar_Ort(item.Nom_Agrup);
                }
                return agrupaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// devuelve los datos de la agrupación dado el id de la misma
        /// </summary>
        /// <param name="id">id de la agrupación</param>
        /// <returns> devuelve la Agrupacion del id pasado por parámetro con todos sus datos</returns>
        public Agrupaciones DameAgrupacionxId(int id)
        {
            try
            {
                var agrupacion = rhData.Agrupaciones.Where(x => x.Id_Agrup == id);
                if (agrupacion.Any())
                {

                    agrupacion.FirstOrDefault().Nom_Agrup = TConeccion.Revisar_Ort(agrupacion.FirstOrDefault().Nom_Agrup);
                    return agrupacion.FirstOrDefault();
                }
                else
                {
                    throw new Exception("No existe agrupación con este id: " + id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// devuelve los datos de la agrupación dado el nombre de la misma
        /// </summary>
        /// <param name="nombre">nombre de la agrupación</param>
        /// <returns> devuelve la Agrupacion del nombre pasado por parámetro con todos sus datos</returns>
        public Agrupaciones DameAgrupacionxNombre(string nombre)
        {
            try
            {
                var agrupacion = rhData.Agrupaciones.Where(x => x.Nom_Agrup.Equals(nombre));
                if (agrupacion.Any())
                {

                    agrupacion.FirstOrDefault().Nom_Agrup = TConeccion.Revisar_Ort(agrupacion.FirstOrDefault().Nom_Agrup);
                    return agrupacion.FirstOrDefault();
                }
                else
                {
                    throw new Exception("No existe agrupación con este nombre: " + nombre);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Devuelve el nombre del departamento de un trabajador
        /// </summary>
        /// <param name = "expediente" > número de solapín del trabajador</param>
        /// <returns>devuelve una cadena con el nombre del departamento</returns>
        public string DameNombreDepartamentoPersonaxExp(int expediente)
        {
            try
            {
                var nombreDept = rhData.Departamentos.Where(x => rhData.Plantilla.Where(y => rhData.Personal.Where(z => z.Exp == expediente).FirstOrDefault().IdPlaza == y.Cod_Plaza).FirstOrDefault().Departamento == x.Id_Dpto).FirstOrDefault().Nom_Dpto;
                nombreDept = TConeccion.Revisar_Ort(nombreDept);
                return nombreDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// devuelve las trazas de una lista de trabajadores en un rango de tiempo dado 
        /// </summary>
        /// <param name="ini">fecha inicial del rango</param>
        /// <param name="fin">fecha final del rango</param>
        /// <param name="expedientes">arreglo de enteros que son cada uno de los expedientes</param>
        /// <returns>devuelve una lista de trazas ordenadas por fecha de los expedientes de la lista en el rango de tiempo dado</returns>
        public List<RegistroESpejo> TrazasEnRangoxListadoExp(DateTime ini, DateTime fin, int[] expedientes)
        {
            List<RegistroESpejo> resp = new List<RegistroESpejo>();
            try
            {
                foreach (var item in expedientes)
                {
                    var trazas = lhData.RegistroESpejo.Where(x => x.Exp == item && x.Fecha >= ini && x.Fecha <= fin).OrderBy(y => y.Fecha).ToList();
                    if (trazas.Any())
                        resp.AddRange(trazas);
                }
            }
            catch (Exception ex)
            {
                EnviarCorreo("192.168.0.19", "ObtenerTRazasExpsEnRango", "carlo.lizano@cie.cu", "ERROR", "en Función copia de trazas a RegistroEspejo: " + ex.Message);
            }
            return resp.OrderBy(t => t.Exp).ToList();

        }

        /// <summary>
        /// Método para enviar correos automáticos desde el código
        /// </summary>
        /// <param name="server">cadena con la dirección del servidor, actualmente en el cie sería: 192.168.0.19</param>
        /// <param name="emisor">cadena con la dirección del que envía el correo ejm:"correoautomatico@cie.cu"  </param>
        /// <param name="receptor">cadena con la dirección del remitente ejm: carlo.lizano@cie.cu</param>
        /// <param name="asunto">cadena para insertar un Asunto en el correo</param>
        /// <param name="message">cadena para introducir texto en el correo</param>
        private static void EnviarCorreo(string server, string emisor, string receptor, string asunto, string message)
        {

            //La cadena "servidor" es el servidor de correo que enviará tu mensaje

            // Crea el mensaje estableciendo quién lo manda y quién lo recibe
            MailMessage mensaje = new MailMessage(emisor, receptor, asunto, message);
            SmtpClient cliente = new SmtpClient(server)
            {
                //Añade credenciales si el servidor lo requiere.
                Credentials = CredentialCache.DefaultNetworkCredentials
            };
            //Aqui lo envia
            cliente.Send(mensaje);
        }

        /// <summary>
        /// Devuelve la categoría ocupacional de un trabajador
        /// </summary>
        /// <param name = "expediente" > número de solapín del trabajador</param>
        /// <returns>devuelve una cadena con la categoría ocupacional</returns>
        public string DameCatOcupacionalxExp(int expediente)
        {
            try
            {
                var nombreCatOcu = rhData.COcupacional.Find(rhData.Personal.Where(x => x.Exp == expediente).FirstOrDefault().CategOcup).Nomb_CO ;
                nombreCatOcu = TConeccion.Revisar_Ort(nombreCatOcu);
                return nombreCatOcu;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Devuelve la categoría ocupacional de un trabajador de baja
        /// </summary>
        /// <param name = "expediente" > número de solapín del trabajador</param>
        /// <returns>devuelve una cadena con la categoría ocupacional</returns>
        public string DameCatOcupacionalxExpBaja(int expediente)
        {
            try
            {
                var nombreCatOcu = rhData.COcupacional.Find(rhData.BajasPers.Where(x => x.Exp == expediente).FirstOrDefault().CategOcup).Nomb_CO;
                nombreCatOcu = TConeccion.Revisar_Ort(nombreCatOcu);
                return nombreCatOcu;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Busca los datos de una persona dado el exp y si no aparece activo busca en las bajas del centro.
        /// </summary>
        /// <param name="expediente">int que responde al solapin</param>
        /// <returns>retorna un PERSONALRH con los datos de la persona que tiene ese solapín </returns>
        public PersonalRH DamePersonaxExpDeep(int expediente)
        {
            try
            {
                var personal = rhData.Personal.Where(x => x.Exp == expediente);
                if (personal.Any())
                {
                    foreach (var item in personal)
                    {
                        item.Nombre = TConeccion.Revisar_Ort(item.Nombre);
                        item.Apellido1 = TConeccion.Revisar_Ort(item.Apellido1);
                        item.Apellido2 = TConeccion.Revisar_Ort(item.Apellido2);
                        item.Direccion = TConeccion.Revisar_Ort(item.Direccion);
                    }
                    return personal.FirstOrDefault();
                }
                else
                {
                    //busquemos en las bajas..

                    var personalBaja = rhData.BajasPers.Where(x => x.Exp == expediente).OrderByDescending(x => x.Baja);
                    if (personalBaja.Any())
                    {
                        return new PersonalRH()
                        {
                            Exp = personalBaja.FirstOrDefault().Exp,
                            Nombre = TConeccion.Revisar_Ort(personalBaja.FirstOrDefault().Nombre) + " (BAJA)",
                            Apellido1 = TConeccion.Revisar_Ort(personalBaja.FirstOrDefault().Apellido1) + " (BAJA)",
                            Apellido2 = TConeccion.Revisar_Ort(personalBaja.FirstOrDefault().Apellido2) + " (BAJA)",
                            CarneId = personalBaja.FirstOrDefault().CarneId,
                            Direccion = TConeccion.Revisar_Ort(personalBaja.FirstOrDefault().Direccion),
                            Telefono = personalBaja.FirstOrDefault().Telefono,

                        };
                    }
                    else
                    {
                        throw new Exception("No existe o ha existido trabajador con el solapín : " + expediente);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca los datos de una persona dado el carnet de identidad y si no aparece activo busca en las bajas del centro.
        /// </summary>
        /// <param name="ci">string de once dígitos que responde al carnet de identidad</param>
        /// <returns>retorna un PERSONALRH con los datos de la persona que tiene ese carnet </returns>
        public PersonalRH DamePersonaxCiDeep(string ci)
        {
            try
            {
                var personal = rhData.Personal.Where(x => x.CarneId.Equals(ci));
                personal.ToList();
                if (personal.Any())
                {
                    foreach (var item in personal)
                    {
                        item.Nombre = TConeccion.Revisar_Ort(item.Nombre);
                        item.Apellido1 = TConeccion.Revisar_Ort(item.Apellido1);
                        item.Apellido2 = TConeccion.Revisar_Ort(item.Apellido2);
                        item.Direccion = TConeccion.Revisar_Ort(item.Direccion);
                    }
                    return personal.FirstOrDefault();
                }
                else
                {
                    //busquemos en las bajas..

                    var personalBaja = rhData.BajasPers.Where(x => x.CarneId.Equals(ci)).OrderByDescending(x => x.Baja);
                    if (personalBaja.Any())
                    {
                        return new PersonalRH()
                        {
                            Exp = personalBaja.FirstOrDefault().Exp,
                            Nombre = TConeccion.Revisar_Ort(personalBaja.FirstOrDefault().Nombre) + " (BAJA)",
                            Apellido1 = TConeccion.Revisar_Ort(personalBaja.FirstOrDefault().Apellido1) + " (BAJA)",
                            Apellido2 = TConeccion.Revisar_Ort(personalBaja.FirstOrDefault().Apellido2) + " (BAJA)",
                            CarneId = personalBaja.FirstOrDefault().CarneId,
                            Direccion = TConeccion.Revisar_Ort(personalBaja.FirstOrDefault().Direccion),
                            Telefono = personalBaja.FirstOrDefault().Telefono,

                        };
                    }
                    else
                    {
                        throw new Exception("No existe o ha existido trabajador con ese CI : " + ci);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Devuelve una lista con las personas que cumplen año en el rango de fechas pasado por parámetros.
        /// </summary>
        /// <param name="inicial"> fecha inicial del rango</param>
        /// <param name="final">fecha final del rango</param>
        /// <returns>devuelve una lista de Personal o una excepción si no existen cunpleaños en ese rango de fecha</returns>
        public List<Personal> Cumples(DateTime inicial, DateTime final)
        {
            try
            {
                if (final < inicial)
                {
                    throw new Exception("La fecha Inicial debe ser menor que la final");
                }
                var personal = lhData.Personal.ToList();

                var cumplen = personal.ToList().FindAll(t =>
                              FechadeCarnet(t.CarneId, inicial).Date > inicial.Date && FechadeCarnet(t.CarneId, final).Date < final.Date ||
                              (FechadeCarnet(t.CarneId, inicial).Date == inicial.Date || FechadeCarnet(t.CarneId, final).Date == final.Date));
                if (cumplen.Any())
                {
                    return cumplen;
                }
                else
                {
                    throw new Exception("No existen cumples en este rango");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método auxiliar que devuelve la fecha de nacimiento dado un carnet de identidad.
        /// </summary>
        /// <param name="p">string de once dígitos que responde al carnet de identidad</param>
        /// <param name="pivo">fecha utiliada solo para el año</param>
        /// <returns> devuelve la fecha del cumpleaño pero en el año de la variable pasada por parámetro.</returns>
        private static DateTime FechadeCarnet(string p, DateTime pivo)
        {
            return new DateTime(pivo.Year, int.Parse(p.Substring(2, 2)), int.Parse(p.Substring(4, 2)));
        }

        /// <summary>
        /// Método para obtener la cuantia que recibe una persona en base a su grado científico 
        /// </summary>
        /// <param name="expediente">entero que responde al solapín del trabajador</param>
        /// <returns> devuelve la cuantia que cobra el trabajador en base a  su grado científico.</returns>
        public decimal CuantiaRetornar (int expediente)
        {
            var persona = rhData.Personal.Where(x => x.Exp == expediente).FirstOrDefault();
            if (persona.GradoCient != null)
            return (decimal) rhData.GCientifico.Find(persona.GradoCient).Cuantia;
            else
                return 0;
        }

        /// <summary>
        /// Método para obtener la cantidad de años completos que un trabajador lleva trabajando en el centro 
        /// </summary>
        /// <param name="expediente">entero que responde al solapín del trabajador</param>
        /// <returns> devuelve la cantidad de años trabajados.</returns>
        public int AñosTrabajados(int expediente)
        {
            var persona = DamePersonaxExpDeep(expediente);
            var tiempoTranscurrido = DateTime.Now - persona.Alta;
            return Convert.ToInt32(Math.Floor((tiempoTranscurrido.TotalDays / 365)));
        }

        /// <summary>
        /// Método para obtener la plaza que ocupa el trabajador en el centro 
        /// </summary>
        /// <param name="expediente">entero que responde al solapín del trabajador</param>
        /// <returns> devuelve el nombre de la plaza que ocupa el trabajador</returns>
        public string Cargo(int expediente)
        {
            var plaza = rhData.Personal.Where(x => x.Exp == expediente).SingleOrDefault().IdPlaza;
            var plantilla = rhData.Plantilla.Where(x => x.Cod_Plaza == plaza).SingleOrDefault();
            return TConeccion.Revisar_Ort(rhData.Plazas.Where( x => x.Id_Ocup == plantilla.Ocupacion).SingleOrDefault().Nom_Ocup);
        }

        public double SalarioBase(int expediente)
        {
            double salarioBase = 0;
            var persona = rhData.Personal.Where(x => x.Exp == expediente).SingleOrDefault();
            if (persona != null)
            {
                var plantilla = rhData.Plantilla.Where(x => x.Cod_Plaza == persona.IdPlaza).SingleOrDefault();
                salarioBase = (double)(plantilla.Basico + plantilla.Condiciones + plantilla.CTecnica + plantilla.RCITMA + plantilla.EspecialistaP + plantilla.CTecnologica);
            }
            else
            {
                var personaBaja = rhData.BajasPers.Where(x => x.Exp == expediente).ToList().FirstOrDefault();
                var plantillaBaja = rhData.BajaPlantilla.Where(x => x.Cod_Plaza == personaBaja.IdPlaza).SingleOrDefault();
                salarioBase = (double)(plantillaBaja.Basico + plantillaBaja.Condiciones + plantillaBaja.CTecnica + plantillaBaja.RCITMA + plantillaBaja.EspecialistaP + plantillaBaja.CTecnologica);
            }
            return salarioBase;
        }

        public List<RegistroESpejo> Registros(int expediente, DateTime fecha)
        {
            List<RegistroESpejo> registros = new List<RegistroESpejo>();
            registros = lhData.RegistroESpejo.Where(x => x.Exp == expediente && x.Fecha.Day == fecha.Day && x.Fecha.Month == fecha.Month && x.Fecha.Year == fecha.Year).ToList();
            return registros.OrderBy(x => x.Fecha).ToList();
        }

        public List<AltasBajas> AltasxAño(int año, int mes)
        {
            List<AltasBajas> altas = new List<AltasBajas>();
            
            if (mes == 0)
            {
                var altasActivas = rhData.Personal.Where(x => x.Alta.Year == año).ToList();
                var altasBajas = rhData.BajasPers.Where(x => ((DateTime)x.Alta).Year == año).ToList();

                foreach (var item in altasActivas)
                {
                    var alta = new AltasBajas();
                    alta.Exp = item.Exp;
                    alta.Nombre = TConeccion.Revisar_Ort(item.Nombre + " " + item.Apellido1 + " " + item.Apellido2);
                    alta.Plaza = TConeccion.Revisar_Ort(rhData.Plazas.Find(rhData.Plantilla.Find(item.IdPlaza).Ocupacion).Nom_Ocup);
                    alta.Sexo = item.CarneId[9] % 2 == 0 ? 'M' : 'F';
                    if ((int.Parse(item.CarneId.Substring(0, 2))) >= 0 && (int.Parse(item.CarneId.Substring(0, 2))) <= int.Parse(DateTime.Now.Year.ToString().Substring(2,2)))
                    {
                        DateTime fechaNacimiento = new DateTime(int.Parse("20" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                        alta.Edad = (int)(item.Alta - fechaNacimiento).TotalDays / 365;
                    }
                    else
                    {
                        DateTime fechaNacimiento = new DateTime(int.Parse("19" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                        alta.Edad = (int)(item.Alta - fechaNacimiento).TotalDays / 365;
                    }
                    alta.Fecha = item.Alta;
                    alta.Raza = item.Raza;
                    altas.Add(alta);
                }

                foreach (var item in altasBajas)
                {
                    if (((DateTime)item.Alta).Year != (((DateTime)item.Baja).Year) || (((DateTime)item.Alta).DayOfYear != ((DateTime)item.Baja).DayOfYear && ((DateTime)item.Alta).DayOfYear < ((DateTime)item.Baja).DayOfYear))
                    {
                        var baja = new AltasBajas();
                        baja.Exp = item.Exp;
                        baja.Nombre = TConeccion.Revisar_Ort(item.Nombre + " " + item.Apellido1 + " " + item.Apellido2);
                        baja.Plaza = TConeccion.Revisar_Ort(rhData.Plazas.Find(rhData.BajaPlantilla.Find(item.IdPlaza).Ocupacion).Nom_Ocup);
                        baja.Sexo = item.CarneId[9] % 2 == 0 ? 'M' : 'F';
                        baja.Sexo = item.CarneId[9] % 2 == 0 ? 'M' : 'F';
                        if ((int.Parse(item.CarneId.Substring(0, 2))) >= 0 && (int.Parse(item.CarneId.Substring(0, 2))) <= int.Parse(DateTime.Now.Year.ToString().Substring(2, 2)))
                        {
                            DateTime fechaNacimiento = new DateTime(int.Parse("20" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                            baja.Edad = (int)(((TimeSpan)(item.Alta - fechaNacimiento)).TotalDays) / 365;
                        }
                        else
                        {
                            DateTime fechaNacimiento = new DateTime(int.Parse("19" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                            baja.Edad = (int)(((TimeSpan)(item.Alta - fechaNacimiento)).TotalDays) / 365;
                        }
                        baja.Fecha = ((DateTime)item.Alta);
                        altas.Add(baja);
                    }
                }
            }
            else
            {
                var altasActivas = rhData.Personal.Where(x => x.Alta.Year == año && x.Alta.Month == mes).ToList();
                var altasBajas = rhData.BajasPers.Where(x => ((DateTime)x.Alta).Year == año && ((DateTime)x.Alta).Month == mes).ToList();

                foreach (var item in altasActivas)
                {
                    var alta = new AltasBajas();
                    alta.Exp = item.Exp;
                    alta.Nombre = TConeccion.Revisar_Ort(item.Nombre + " " + item.Apellido1 + " " + item.Apellido2);
                    alta.Plaza = TConeccion.Revisar_Ort(rhData.Plazas.Find(rhData.Plantilla.Find(item.IdPlaza).Ocupacion).Nom_Ocup);
                    alta.Sexo = item.CarneId[9] % 2 == 0 ? 'M' : 'F';
                    if ((int.Parse(item.CarneId.Substring(0, 2))) >= 0 && (int.Parse(item.CarneId.Substring(0, 2))) <= int.Parse(DateTime.Now.Year.ToString().Substring(2, 2)))
                    {
                        DateTime fechaNacimiento = new DateTime(int.Parse("20" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                        alta.Edad = (int)(item.Alta - fechaNacimiento).TotalDays / 365;
                    }
                    else
                    {
                        DateTime fechaNacimiento = new DateTime(int.Parse("19" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                        alta.Edad = (int)(item.Alta - fechaNacimiento).TotalDays / 365;
                    }
                    alta.Fecha = item.Alta;
                    alta.Raza = item.Raza;
                    altas.Add(alta);
                }

                foreach (var item in altasBajas)
                {
                    if (((DateTime)item.Alta).Year != (((DateTime)item.Baja).Year) || (((DateTime)item.Alta).DayOfYear != ((DateTime)item.Baja).DayOfYear && ((DateTime)item.Alta).DayOfYear < ((DateTime)item.Baja).DayOfYear))
                    {
                        var baja = new AltasBajas();
                        baja.Exp = item.Exp;
                        baja.Nombre = TConeccion.Revisar_Ort(item.Nombre + " " + item.Apellido1 + " " + item.Apellido2);
                        baja.Plaza = TConeccion.Revisar_Ort(rhData.Plazas.Find(rhData.BajaPlantilla.Find(item.IdPlaza).Ocupacion).Nom_Ocup);
                        baja.Sexo = item.CarneId[9] % 2 == 0 ? 'M' : 'F';
                        if ((int.Parse(item.CarneId.Substring(0, 2))) >= 0 && (int.Parse(item.CarneId.Substring(0, 2))) <= int.Parse(DateTime.Now.Year.ToString().Substring(2, 2)))
                        {
                            DateTime fechaNacimiento = new DateTime(int.Parse("20" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                            baja.Edad = (int)(((TimeSpan)(item.Alta - fechaNacimiento)).TotalDays) / 365;
                        }
                        else
                        {
                            DateTime fechaNacimiento = new DateTime(int.Parse("19" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                            baja.Edad = (int)(((TimeSpan)(item.Alta - fechaNacimiento)).TotalDays) / 365;
                        }
                        baja.Fecha = ((DateTime)item.Alta);
                        altas.Add(baja);
                    }                    
                }
            }

            altas = altas.OrderBy(x => x.Exp).ToList();
            if (altas.Any())
            {
                var pivote = altas.FirstOrDefault();
                var auxiliar = new List<AltasBajas>();
                foreach (var item in altas)
                {
                    if (item.Exp == pivote.Exp && !item.Equals(pivote))
                    {
                        auxiliar.Add(item);                        
                    }
                    pivote = item;
                }
                foreach (var item in auxiliar)
                {
                    altas.Remove(item);
                }
            }            
            return altas.OrderBy(x => x.Fecha).ToList();
        }

        public List<Bajas> BajasxAño(int año, int mes)
        {
            List<Bajas> bajas = new List<Bajas>();

            if (mes == 0)
            {
                var altasBajas = rhData.BajasPers.Where(x => ((DateTime)x.Baja).Year == año).ToList();

                foreach (var item in altasBajas)
                {
                    if (((DateTime)item.Alta).Year != (((DateTime)item.Baja).Year) || (((DateTime)item.Alta).DayOfYear != ((DateTime)item.Baja).DayOfYear && ((DateTime)item.Alta).DayOfYear < ((DateTime)item.Baja).DayOfYear))
                    {
                        var baja = new Bajas();
                        baja.Exp = item.Exp;
                        baja.Nombre = TConeccion.Revisar_Ort(item.Nombre + " " + item.Apellido1 + " " + item.Apellido2);
                        baja.catOcup = TConeccion.Revisar_Ort(rhData.COcupacional.Find(item.CategOcup).Nomb_CO).ToCharArray().First();
                        baja.Sexo = item.CarneId[9] % 2 == 0 ? 'M' : 'F';
                        baja.Sexo = item.CarneId[9] % 2 == 0 ? 'M' : 'F';
                        if ((int.Parse(item.CarneId.Substring(0, 2))) >= 0 && (int.Parse(item.CarneId.Substring(0, 2))) <= int.Parse(DateTime.Now.Year.ToString().Substring(2, 2)))
                        {
                            DateTime fechaNacimiento = new DateTime(int.Parse("20" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                            baja.Edad = (int)(((TimeSpan)(item.Baja - fechaNacimiento)).TotalDays) / 365;
                        }
                        else
                        {
                            DateTime fechaNacimiento = new DateTime(int.Parse("19" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                            baja.Edad = (int)(((TimeSpan)(item.Baja - fechaNacimiento)).TotalDays) / 365;
                        }
                        baja.FechaAlta = ((DateTime)item.Alta);
                        baja.FechaBaja = ((DateTime)item.Baja);
                        if (item.Motivo != null || item.Motivo != "")
                        {
                            baja.motivo = TConeccion.Revisar_Ort(item.Motivo);
                        }
                        else
                        {
                            baja.motivo = item.Motivo;
                        }
                        bajas.Add(baja);
                    }
                }
            }
            else
            {
                var altasBajas = rhData.BajasPers.Where(x => ((DateTime)x.Baja).Year == año && ((DateTime)x.Baja).Month == mes).ToList();

                foreach (var item in altasBajas)
                {
                    var baja = new Bajas();
                    baja.Exp = item.Exp;
                    baja.Nombre = TConeccion.Revisar_Ort(item.Nombre + " " + item.Apellido1 + " " + item.Apellido2);
                    baja.catOcup = TConeccion.Revisar_Ort(rhData.COcupacional.Find(item.CategOcup).Nomb_CO).ToCharArray().First();
                    baja.Sexo = item.CarneId[9] % 2 == 0 ? 'M' : 'F';
                    baja.Sexo = item.CarneId[9] % 2 == 0 ? 'M' : 'F';
                    if ((int.Parse(item.CarneId.Substring(0, 2))) >= 0 && (int.Parse(item.CarneId.Substring(0, 2))) <= int.Parse(DateTime.Now.Year.ToString().Substring(2, 2)))
                    {
                        DateTime fechaNacimiento = new DateTime(int.Parse("20" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                        baja.Edad = (int)(((TimeSpan)(item.Baja - fechaNacimiento)).TotalDays) / 365;
                    }
                    else
                    {
                        DateTime fechaNacimiento = new DateTime(int.Parse("19" + item.CarneId.Substring(0, 2)), int.Parse(item.CarneId.Substring(2, 2)), int.Parse(item.CarneId.Substring(4, 2)));
                        baja.Edad = (int)(((TimeSpan)(item.Baja - fechaNacimiento)).TotalDays) / 365;
                    }
                    baja.FechaAlta = ((DateTime)item.Alta);
                    baja.FechaBaja = ((DateTime)item.Baja);
                    if (item.Motivo != null)
                    {
                        baja.motivo = TConeccion.Revisar_Ort(item.Motivo);
                    }
                    else
                    {
                        baja.motivo = item.Motivo;
                    }
                    
                    bajas.Add(baja);
                }
            }

            bajas = bajas.OrderBy(x => x.Exp).ToList();
            if (bajas.Any())
            {
                var pivote = bajas.FirstOrDefault();
                var auxiliar = new List<Bajas>();
                foreach (var item in bajas)
                {
                    if (item.Exp == pivote.Exp && !item.Equals(pivote))
                    {
                        auxiliar.Add(item);
                    }
                    pivote = item;
                }
                foreach (var item in auxiliar)
                {
                    bajas.Remove(item);
                }
            }
            return bajas.OrderBy(x => x.FechaBaja).ToList();
        }

        public List<Categorias> Categorizacion(string tipo)
        {
            List<Categorias> categorias = new List<Categorias>();

            switch (tipo)
            {
                case "Biotecnológicas":
                    {
                        foreach (var item in rhData.CBiotecnologicas)
                        {
                            if (item.Nomb_CBT != null && !item.Nomb_CBT.Equals(" "))
                            {
                                var categoria = new Categorias();
                                categoria.Tipo = "Biotecnologica";
                                categoria.Nombre = TConeccion.Revisar_Ort(item.Nomb_CBT);
                                var personas = rhData.Personal.Where(x => x.CategBTecno == item.Cod_CBT).ToList();
                                categoria.Total = personas.Count;
                                //categoria.CantidadCDS = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo Superior")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadCD = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadCE = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Ejecutivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadTecnico = categoria.Total - categoria.CantidadCDS - categoria.CantidadCD - categoria.CantidadCE;
                                categoria.CantidadH = personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 == 0).ToList().Count;
                                categoria.CantidadM = personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 != 0).ToList().Count;
                                categorias.Add(categoria);
                            }
                        }
                    }
                    break;
                case "Docentes":
                    {
                        foreach (var item in rhData.CDocente)
                        {
                            if (item.Nomb_CD != null && !item.Nomb_CD.Equals(" "))
                            {
                                var categoria = new Categorias();
                                categoria.Tipo = "Docente";
                                categoria.Nombre = TConeccion.Revisar_Ort(item.Nomb_CD);
                                var personas = rhData.Personal.Where(x => x.CategDoc == item.Cod_CD).ToList();
                                categoria.Total = personas.Count;
                                //categoria.CantidadCDS = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo Superior")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadCD = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadCE = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Ejecutivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadTecnico = categoria.Total - categoria.CantidadCDS - categoria.CantidadCD - categoria.CantidadCE;
                                categoria.CantidadH = personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 == 0).ToList().Count;
                                categoria.CantidadM = personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 != 0).ToList().Count;
                                categorias.Add(categoria);
                            }
                        }
                    }
                    break;
                case "Tecnológicas":
                    {
                        foreach (var item in rhData.CTecnologicas)
                        {
                            if (item.Nomb_CT != null && !item.Nomb_CT.Equals(" "))
                            {
                                var categoria = new Categorias();
                                categoria.Tipo = "Técnologica";
                                categoria.Nombre = TConeccion.Revisar_Ort(item.Nomb_CT);
                                var personas = rhData.Personal.Where(x => x.CategTecno == item.Cod_CT).ToList();
                                categoria.Total = personas.Count;
                                //categoria.CantidadCDS = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo Superior")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadCD = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadCE = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Ejecutivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadTecnico = categoria.Total - categoria.CantidadCDS - categoria.CantidadCD - categoria.CantidadCE;
                                categoria.CantidadH = personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 == 0).ToList().Count;
                                categoria.CantidadM = personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 != 0).ToList().Count;
                                categorias.Add(categoria);
                            }
                        }
                    }
                    break;
                case "Científicas":
                    {
                        foreach (var item in rhData.CCientificas)
                        {
                            if (item.Nomb_CC != null && !item.Nomb_CC.Equals(" "))
                            {
                                var categoria = new Categorias();
                                categoria.Tipo = "Científica";
                                categoria.Nombre = TConeccion.Revisar_Ort(item.Nomb_CC);
                                var personas = rhData.Personal.Where(x => x.CategCient == item.Cod_CC).ToList();
                                categoria.Total = personas.Count;
                                //categoria.CantidadCDS = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo Superior")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadCD = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadCE = personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Ejecutivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                categoria.CantidadTecnico = categoria.Total - categoria.CantidadCDS - categoria.CantidadCD - categoria.CantidadCE;
                                categoria.CantidadH = personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 == 0).ToList().Count;
                                categoria.CantidadM = personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 != 0).ToList().Count;
                                categorias.Add(categoria);
                            }
                        }
                    }
                    break;
                case "Grado Científico":
                    {
                        var doctores = new Categorias
                        {
                            Tipo = "Grado Científico",
                            Nombre = "Doctores en Ciencia"
                        };
                        var master = new Categorias
                        {
                            Tipo = "Grado Científico",
                            Nombre = "Máster en Ciencias"
                        };
                        var espPostg = new Categorias
                        {
                            Tipo = "Grado Científico",
                            Nombre = "Especialistas de Postgrado"
                        };

                        foreach (var item in rhData.GCientifico)
                        {

                            if (item.Des_GC != null && !item.Des_GC.Equals(" "))
                            {

                                var personas = rhData.Personal.Where(x => x.GradoCient == item.Cod_GC).ToList();

                                if (TConeccion.Revisar_Ort(item.Des_GC.ToLower()).Contains("doctor"))
                                {
                                    doctores.Total += personas.Count;
                                    //doctores.CantidadCDS += personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo Superior")).SingleOrDefault().Cod_CO).ToList().Count;
                                    doctores.CantidadCD += personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                    doctores.CantidadCE += personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Ejecutivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                    doctores.CantidadTecnico = (doctores.Total - doctores.CantidadCDS - doctores.CantidadCD - doctores.CantidadCE);
                                    doctores.CantidadH += personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 == 0).ToList().Count;
                                    doctores.CantidadM += personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 != 0).ToList().Count;
                                }
                                else if (TConeccion.Revisar_Ort(item.Des_GC.ToLower()).Contains("máster") || (TConeccion.Revisar_Ort(item.Des_GC.ToLower()).Contains("master")))
                                {
                                    master.Total += personas.Count;
                                    //master.CantidadCDS += personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo Superior")).SingleOrDefault().Cod_CO).ToList().Count;
                                    master.CantidadCD += personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                    master.CantidadCE += personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Ejecutivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                    master.CantidadTecnico = master.Total - master.CantidadCDS - master.CantidadCD - master.CantidadCE;
                                    master.CantidadH += personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 == 0).ToList().Count;
                                    master.CantidadM += personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 != 0).ToList().Count;
                                }
                                else
                                {
                                    espPostg.Total += personas.Count;
                                    //espPostg.CantidadCDS += personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo Superior")).SingleOrDefault().Cod_CO).ToList().Count;
                                    espPostg.CantidadCD += personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Directivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                    espPostg.CantidadCE += personas.Where(x => x.CategOcup == rhData.COcupacional.Where(y => y.Nomb_CO.Contains("Ejecutivo")).SingleOrDefault().Cod_CO).ToList().Count;
                                    espPostg.CantidadTecnico = espPostg.Total - espPostg.CantidadCDS - espPostg.CantidadCD - espPostg.CantidadCE;
                                    espPostg.CantidadH += personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 == 0).ToList().Count;
                                    espPostg.CantidadM += personas.Where(x => int.Parse(x.CarneId.Substring(9, 1)) % 2 != 0).ToList().Count;
                                }
                            }
                        }
                        categorias.Add(doctores);
                        categorias.Add(master);
                        categorias.Add(espPostg);
                        return categorias;
                    }
            }
            return categorias;
        }

        public List<PersonalRH> DameTodosTrabajadores ()
        {
            var trabajadores = rhData.Personal.ToList();

            foreach (var item in trabajadores)
            {
                item.Nombre = TConeccion.Revisar_Ort(item.Nombre + " " + item.Apellido1 + " " + item.Apellido2);
                item.Apellido1 = TConeccion.Revisar_Ort(item.Apellido1);
                item.Apellido2 = TConeccion.Revisar_Ort(item.Apellido2);
                item.Direccion = TConeccion.Revisar_Ort(item.Direccion);
            }
            trabajadores.OrderBy(x => x.Nombre).ToList();
            return trabajadores;
        }

        public Dictionary<short, short> DameTodosTrabajadoresxPlantilla(int IdAgrupacion)
        {
            var departamentos = DameDepartamentosxAgrupacion(IdAgrupacion);
            Dictionary<short, short> plantilla = new Dictionary<short, short>();
            foreach (var departamento in departamentos)
            {
                foreach (var item in rhData.Plantilla.Where(x => x.Departamento == departamento.Id_Dpto && x.Vacante == 0).ToDictionary(y => y.Cod_Plaza, z => z.Cod_Plaza))
                {
                    plantilla.Add(item.Key, item.Value);
                }                
            }
            return plantilla;
        }

        public List<Departamentos> DameDepartamentosxAgrupacion(int idAgrupacion)
        {
            return rhData.Departamentos.Where(x => x.Id_Agrup == idAgrupacion).ToList();
        }

        public List<PersonalRH> DameTrabajadoresxDepartamento(Dictionary<short, short> plantilla)
        {
            var personal = rhData.Personal.ToList();
            var personalsalida = new List<PersonalRH>();
            foreach (var item in personal)
            {
                if (plantilla.ContainsKey(item.IdPlaza))
                {
                    item.Nombre = TConeccion.Revisar_Ort(item.Nombre);
                    item.Apellido1 = TConeccion.Revisar_Ort(item.Apellido1);
                    item.Apellido2 = TConeccion.Revisar_Ort(item.Apellido2);
                    item.Direccion = TConeccion.Revisar_Ort(item.Direccion);
                    personalsalida.Add(item);
                }
            }
            return personalsalida;
        }

        public List<DatosInformeP4> InformeP4 ()
        {
            var DatosInforme = new List<DatosInformeP4>();
            return DatosInforme;
        }

        public List<DatosInformeCategorizacion> InformeCategorizacion()
        {
            var DatosInforme = new List<DatosInformeCategorizacion>();
            return DatosInforme;
        }

    }
}
