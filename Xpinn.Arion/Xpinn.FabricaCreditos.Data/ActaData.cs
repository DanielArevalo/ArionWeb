using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Credito
    /// </summary>
    public class ActaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Credito
        /// </summary>
        public ActaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosActas(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       // string sql = "Select * from  V_ACTA_APROBACIONCRED " + filtro;
                        string sql = "Select a.*,c.codacta,to_char(c.fecha,'MM/dd/yyyy')as fecha,u.NOMBRE as USUARIO,p.NOMBREPERFIL from  V_ACTA_APROBACIONCRED a  inner join  acta b  on a.NUMERO_RADICACION=b.NUMERO_RADICACION  inner join  actas_numero c on c.CODACTA=b.CODACTA  inner join usuarios u on u.codusuario=b.cod_usuario inner join perfil_usuario p on p.codperfil=b.codperfil where 1=1" + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                             if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fechaacta = Convert.ToString(resultado["FECHA"]);
                            if (resultado["CODACTA"] != DBNull.Value) entidad.acta = Convert.ToInt64(resultado["CODACTA"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor = Convert.ToString(resultado["NOMBRECODEUDOR"]);
                           
                                     lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditosActas", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosReporte(Credito pCredito, Usuario pUsuario, Int64 Acta)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // string sql = "Select * from  V_ACTA_APROBACIONCRED " + filtro;
                        string sql = "Select a.*,c.codacta,to_char(c.fecha,'MM/dd/yyyy')as fecha from  V_ACTA_APROBACIONCRED a inner join  acta b  on a.NUMERO_RADICACION=b.NUMERO_RADICACION inner join  actas_numero c on c.CODACTA=b.CODACTA where c.CODACTA= " + Acta;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                             if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fechaacta = Convert.ToString(resultado["FECHA"]);
                            if (resultado["CODACTA"] != DBNull.Value) entidad.acta = Convert.ToInt64(resultado["CODACTA"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToInt64(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.desc_tasa = Convert.ToString(resultado["TIPO_TASA"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor = Convert.ToString(resultado["NOMBRECODEUDOR"]);
                            
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditosReporte", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditos(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "Select * from  V_ACTA_APROBACIONCRED a where a.cod_linea_credito !=310 and  a.numero_radicacion not in( select  NUMERO_RADICACION from acta) " + filtro;
                        string sql2 = " order by a.COD_OFICINA asc";

                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor= Convert.ToString(resultado["NOMBRECODEUDOR"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditos", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosUsuarios(Credito pCredito, Usuario pUsuario, String filtro,Int64 oficina)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from  V_ACTA_APROBACIONCRED a where  a.numero_radicacion not in( select NUMERO_RADICACION from acta) " + filtro + "and COD_OFICINA=" + oficina;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                           // if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditosUsuario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditosRestructurados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "Select * from  V_ACTA_APROBACIONCRED a where a.cod_linea_credito=310  and a.numero_radicacion  not in( select  NUMERO_RADICACION from acta) " + filtro;
                        string sql2 = " order by a.COD_OFICINA asc";

                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor = Convert.ToString(resultado["NOMBRECODEUDOR"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarCreditos", ex);
                        return null;
                    }
                }
            }
        }
        // <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarActas(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "select distinct(a.codacta),to_char(b.fecha,'MM/dd/yyyy') as fecha from actas_numero b inner join acta a  on b.codacta=a.codacta" + filtro ;

                        string sql2 = " order by a.codacta desc";

                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["CODACTA"] != DBNull.Value) entidad.acta = Convert.ToInt64(resultado["CODACTA"]);
                           // if (resultado["DESCRIPCION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["DESCRIPCION"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fechaacta = Convert.ToString(resultado["FECHA"]);
        

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ListarActas", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Crea un registro en la tabla Diligencia de la base de datos
        /// </summary>
        /// <param name="pDiligencia">Entidad Diligencia</param>
        /// <returns>Entidad Diligencia creada</returns>
        public Credito CrearActa(Credito pActa, Usuario pUsuario, String idacta, DateTime fechaaprobacion, Int64 codusuario, Int64 codperfil)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pACTA = cmdTransaccionFactory.CreateParameter();
                        pACTA.ParameterName = "P_ACTA";
                        pACTA.Value = pActa.acta;
                        //pACTA.DbType = DbType.VarNumeric;
                        pACTA.DbType = DbType.Int64;
                        pACTA.Direction = ParameterDirection.Output;   

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pActa.numero_radicacion;
                        //pNUMERO_RADICACION.DbType = DbType.String;
                       // pACTA.DbType = DbType.Int64;
                        pNUMERO_RADICACION.Direction = ParameterDirection.Input;


                        DbParameter pIDACTA = cmdTransaccionFactory.CreateParameter();
                        pIDACTA.ParameterName = "p_ID";                       
                        pIDACTA.Direction = ParameterDirection.Input;
                       // pIDACTA.DbType = DbType.String;
                        pIDACTA.Value = idacta;

                        DbParameter p_FECHA = cmdTransaccionFactory.CreateParameter();
                        p_FECHA.ParameterName = "p_FECHA";
                        p_FECHA.Direction = ParameterDirection.Input;
                        p_FECHA.DbType = DbType.Date;
                        p_FECHA.Value = fechaaprobacion;

                        DbParameter P_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_USUARIO.ParameterName = "P_USUARIO";
                        P_USUARIO.Direction = ParameterDirection.Input;                        
                        P_USUARIO.Value = codusuario;


                        DbParameter P_PERFIL = cmdTransaccionFactory.CreateParameter();
                        P_PERFIL.ParameterName = "P_PERFIL";
                        P_PERFIL.Direction = ParameterDirection.Input;
                        P_PERFIL.Value = codperfil; 

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pIDACTA);
                        cmdTransaccionFactory.Parameters.Add(pACTA);
                        cmdTransaccionFactory.Parameters.Add(p_FECHA);
                        cmdTransaccionFactory.Parameters.Add(P_USUARIO);
                        cmdTransaccionFactory.Parameters.Add(P_PERFIL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ACTAS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pActa.acta = Convert.ToInt64(pACTA.Value);
                                  
                        return pActa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "CrearActa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para cargo gerente 
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametrocargoGerente(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=402";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.paramcargo= Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarParametrocargoGerente", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un dato de la tabla general para Cargo Comite de Credito n1
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametrocargoComitedecreditoniv1(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=403";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.paramcargo = Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarParametrocargoComitedecreditoniv1", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un dato de la tabla general para Cargo Comite de Credito n1
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametroReestructurado(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=430";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.paramrestruct = Convert.ToString(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarParametroReestructurado", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para Cargo Comite de Credito n1
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametrocargoComitedecreditoniv4(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=404";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.paramcargo = Convert.ToInt64(resultado["valor"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarParametrocargoComitedecreditoniv4", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un registro de la tabla acta de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Acta consultada</returns>
        public Credito ConsultarActa(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select a.*,c.codacta,to_char(c.fecha,'MM/dd/yyyy')as fecha from  V_ACTA_APROBACIONCRED a inner join  XPINNADM.acta b  on a.NUMERO_RADICACION=b.DESCRIPCION inner join  XPINNADM.actas_numero c on c.CODACTA=b.CODACTA where b.codacta =" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.Codeudor = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["NOMBRECODEUDOR"] != DBNull.Value) entidad.NombreCodeudor = Convert.ToString(resultado["NOMBRECODEUDOR"]);
                           
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarActa", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla acta de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Acta consultada</returns>
        public Credito ConsultarAprobadorActa(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select a.*,c.codacta,to_char(c.fecha,'MM/dd/yyyy')as fecha,u.NOMBRE as USUARIO, p.NOMBREPERFIL from  V_ACTA_APROBACIONCRED a inner join  acta b  on a.NUMERO_RADICACION=b.NUMERO_RADICACION inner join  actas_numero c on c.CODACTA=b.CODACTA  inner join usuarios u  on u.codusuario=b.cod_usuario inner join perfil_usuario p on p.codperfil=b.CODPERFIL where b.codacta =" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                           // if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                           }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActaData", "ConsultarAprobadorActa", ex);
                        return null;
                    }

                }
            }
        }

    }   
}