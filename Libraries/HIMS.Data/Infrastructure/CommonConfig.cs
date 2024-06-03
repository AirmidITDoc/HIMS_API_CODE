using HIMS.Core.Domain.Grid;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.Infrastructure
{
    public class CoomonSqlModel
    {
        public string? Sp_Name { get; set; }
        public SqlParameter[]? Sql_Para { get; set; }
    }

    public class CommonConfig
    {
        public CoomonSqlModel comonParams = new CoomonSqlModel();
        public CommonConfig(string mode, ListRequestModel model)
        {
            string sp_Name = string.Empty;
            SqlParameter[] para = new SqlParameter[0];
            switch (mode)
            {
                #region :: PurchaseOrder ::
                case "PurchaseOrder":
                    {
                        sp_Name = "m_Rtrv_PurchaseOrderList_by_Name_Pagn";
                        para = new SqlParameter[] {
                            new SqlParameter { ParameterName = "@ToStoreId", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = model.ToStoreId},
                            new SqlParameter { ParameterName = "@From_Dt", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = model.From_Dt},
                            new SqlParameter { ParameterName = "@To_Dt", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = model.To_Dt},
                            new SqlParameter { ParameterName = "@IsVerify", SqlDbType = SqlDbType.TinyInt, Direction = ParameterDirection.Input, Value = model.IsVerify},
                            new SqlParameter { ParameterName = "@Supplier_Id", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = model.Supplier_Id},
                            new SqlParameter { ParameterName = "@Start", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = model.Start},
                            new SqlParameter { ParameterName = "@Length", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = model.Length }
                        };

                        break;
                    }
                #endregion

                #region :: GRN ::
                case "GRN":
                    {
                        sp_Name = "m_Rtrv_GRNList_by_Name";
                        para = new SqlParameter[] {
                            new SqlParameter { ParameterName = "@ToStoreId", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = model.ToStoreId},
                            new SqlParameter { ParameterName = "@From_Dt", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = model.From_Dt},
                            new SqlParameter { ParameterName = "@To_Dt", SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input, Value = model.To_Dt},
                            new SqlParameter { ParameterName = "@IsVerify", SqlDbType = SqlDbType.TinyInt, Direction = ParameterDirection.Input, Value = model.IsVerify},
                            new SqlParameter { ParameterName = "@Supplier_Id", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = model.Supplier_Id}
                        };

                        break;
                    }
                #endregion
                default:
                    break;
            }

            comonParams.Sp_Name = sp_Name;
            comonParams.Sql_Para = para;
        }
    }
}
