using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para Persona1
    /// </summary>
    public class PersonaBiometriaBusiness : GlobalData
    {        
        private PersonaBiometriaData DABiometria;
        

        /// <summary>
        /// Constructor del objeto de negocio para Persona_Biometria
        /// </summary>
        public PersonaBiometriaBusiness()
        {
            DABiometria = new PersonaBiometriaData();
            
        }

        public PersonaBiometria CrearPersonaBiometria(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    
                    ts.Complete();
                }
                return pPersonaBiometria;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaBusiness", "CrearPersonaBiometriaPersona", ex);
                return null;
            }
        }

        public PersonaBiometria ModificarPersonaBiometria(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    
                    ts.Complete();
                }
                return pPersonaBiometria;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaBusiness", "ModificarPersonaBiometria", ex);
                return null;
            }
        }

        public PersonaBiometria ConsultarPersonaBiometria(Int64 pId, Int32 pNumeroDedo, Usuario pUsuario)
        {
            try
            {
                return DABiometria.ConsultarPersonaBiometria(pId, pNumeroDedo, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaBusiness", "ConsultarPersonaBiometria", ex);
                return null;
            }
        }

        public List<PersonaBiometria> ListarPersonaBiometria(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            try
            {
                return DABiometria.ListarPersonaBiometria(pPersonaBiometria, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaBusiness", "ListarPersonaBiometria", ex);
                return null;
            }
        }

        public Int64 ExistePersonaBiometria(Int64 pId, Int32 pNumeroDedo, Usuario pUsuario)
        {
            try
            {
                return DABiometria.ExistePersonaBiometria(pId, pNumeroDedo, pUsuario); ;
            }
            catch 
            {
                return -1;
            }
        }

        public List<PersonaBiometria> Handler(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            try
            {
                return DABiometria.Handler(pPersonaBiometria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaBusiness", "Handler", ex);
                return null;
            }
        }

        public PersonaBiometria ConsultarPersonaBiometriaSECUGEN(Int64 pId, Int32 pNumeroDedo, Usuario pUsuario)
        {
            try
            {
                return DABiometria.ConsultarPersonaBiometriaSECUGEN(pId, pNumeroDedo, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaBusiness", "ConsultarPersonaBiometriaSECUGEN", ex);
                return null;
            }
        }

    }
}


