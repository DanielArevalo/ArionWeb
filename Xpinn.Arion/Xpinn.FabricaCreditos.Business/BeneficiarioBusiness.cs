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
    /// Objeto de negocio para Beneficiario
    /// </summary>
    public class BeneficiarioBusiness : GlobalBusiness
    {
        private BeneficiarioData DABeneficiario;

        
        public BeneficiarioBusiness()
        {
            DABeneficiario = new BeneficiarioData();
        }

        




        public void EliminarBeneficiario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABeneficiario.EliminarBeneficiario(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "EliminarBeneficiario", ex);
            }
        }

        
        public List<Beneficiario> ConsultarBeneficiario(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DABeneficiario.ConsultarBeneficiario(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "ConsultarActividad", ex);
                return null;
            }
        }

        

        public List<Beneficiario> ListarBeneficiario(Beneficiario pBeneficiario, Usuario pUsuario)
        {
            try
            {
                return DABeneficiario.ListarBeneficiario(pBeneficiario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "ListarBeneficiario", ex);
                return null;
            }
        }


        public List<Beneficiario> ListarParentesco(Beneficiario pBeneficiario, Usuario pUsuario)
        {
            try
            {
                return DABeneficiario.ListarParentesco(pBeneficiario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "ListarParentesco", ex);
                return null;
            }
        }

        #region BENEFICIARIO POR AHORRO VISTA

        public List<Beneficiario> ConsultarBeneficiarioAhorroVista(String pId, Usuario vUsuario)
        {
            try
            {
                return DABeneficiario.ConsultarBeneficiarioAhorroVista(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "ConsultarActividad", ex);
                return null;
            }
        }

        #endregion

        #region BENEFICIARIO POR AHORRO PROGRAMAD

        public void EliminarBeneficiarioAhorroProgramado(long pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABeneficiario.EliminarBeneficiarioAhorroProgramado(pId, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "EliminarBeneficiario", ex);
            }
        }

        #endregion

        #region BENEFICIARIO DE APORTE

        public void EliminarBeneficiarioAporte(long pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABeneficiario.EliminarBeneficiarioAporte(pId, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "EliminarBeneficiarioAporte", ex);
            }
        }

        #endregion 

    }
}