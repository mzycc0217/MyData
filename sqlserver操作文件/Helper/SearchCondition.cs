using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TestJwt.Helper
{
    public class SearchCondition
    {

        private List<Condition> list = new List<Condition>();
        /// <summary>
        /// 添加条件
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="sqlOperator">操作符</param>
        /// <returns></returns>
        public SearchCondition AddCondition(string name, string value, SqlOperator sqlOperator)
        {
            list.Add(new Condition(name, value, sqlOperator));
            return this;
        }

        public string BuildConditionSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" WHERE 1 = 1 ");
            if (list == null || list.Count == 0)
            {
                return sql.ToString();
            }
            foreach (var item in list)
            {
                sql.Append(" AND " + item.Name);
                if (item.Operator == SqlOperator.Like)
                {
                    sql.AppendFormat(" LIKE '%{0}%'", item.Value);
                }
                else if (item.Operator == SqlOperator.NotLike)
                {
                    sql.AppendFormat(" NOT LIKE '%{0}%'", item.Value);
                }
                else if (item.Operator == SqlOperator.LikeStartAt)
                {
                    sql.AppendFormat(" LIKE '{0}%'", item.Value);
                }
                else if (item.Operator == SqlOperator.Equal)
                {
                    sql.AppendFormat(" = '{0}'", item.Value);
                }
                else if (item.Operator == SqlOperator.NotEqual)
                {
                    sql.AppendFormat(" <> '{0}'", item.Value);
                }
                else if (item.Operator == SqlOperator.MoreThan)
                {
                    sql.AppendFormat(" > '{0}'", item.Value);
                }
                else if (item.Operator == SqlOperator.LessThan)
                {
                    sql.AppendFormat(" < '{0}'", item.Value);
                }
                else if (item.Operator == SqlOperator.MoreThanOrEqual)
                {
                    sql.AppendFormat(" >= '{0}'", item.Value);
                }
                else if (item.Operator == SqlOperator.LessThanOrEqual)
                {
                    sql.AppendFormat(" <= '{0}'", item.Value);
                }
                else if (item.Operator == SqlOperator.In)
                {
                    sql.AppendFormat(" IN ({0})", item.Value);
                }

            }
            return sql.ToString();
        }

        public string BuildConditionParameterSql()
        {
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sql = new StringBuilder();
            sql.Append(" WHERE  1 = 1 ");
            foreach (var item in list)
            {
                sql.Append(" AND " + item.Name);
                if (item.Operator == SqlOperator.Like)
                {
                    sql.AppendFormat(" LIKE @{0}", item.Name);
                }
                else if (item.Operator == SqlOperator.NotLike)
                {
                    sql.AppendFormat(" NOT LIKE  @{0}", item.Name);
                }
                else if (item.Operator == SqlOperator.LikeStartAt)
                {
                    sql.AppendFormat(" LIKE  @{0}", item.Name);
                }
                else if (item.Operator == SqlOperator.Equal)
                {
                    sql.AppendFormat(" =  @{0}", item.Name);
                }
                else if (item.Operator == SqlOperator.NotEqual)
                {
                    sql.AppendFormat(" <> @{0}", item.Name);
                }
                else if (item.Operator == SqlOperator.MoreThan)
                {
                    sql.AppendFormat(" > @{0}", item.Name);
                }
                else if (item.Operator == SqlOperator.LessThan)
                {
                    sql.AppendFormat(" < @{0}", item.Name);
                }
                else if (item.Operator == SqlOperator.MoreThanOrEqual)
                {
                    sql.AppendFormat(" >= @{0}", item.Name);
                }
                else if (item.Operator == SqlOperator.LessThanOrEqual)
                {
                    sql.AppendFormat(" <= @{0}", item.Name);
                }
                else if (item.Operator == SqlOperator.In)
                {
                    sql.AppendFormat(" IN @{0}", item.Name);
                }

            }
            return sql.ToString();
        }

        public List<SqlParameter> BuildConditionParameterList()
        {
            List<SqlParameter> listParameters = new List<SqlParameter>();
            foreach (var item in list)
            {
                SqlParameter parameter = new SqlParameter("@" + item.Name, item.Value);

                if (item.Operator == SqlOperator.Like)
                {
                    parameter = new SqlParameter("@" + item.Name, "%" + item.Value + "%");
                    parameter.SqlDbType = SqlDbType.NVarChar;
                }
                else if (item.Operator == SqlOperator.NotLike)
                {
                    parameter = new SqlParameter("@" + item.Name, "%" + item.Value + "%");
                    parameter.SqlDbType = SqlDbType.NVarChar;
                }
                else if (item.Operator == SqlOperator.LikeStartAt)
                {
                    parameter = new SqlParameter("@" + item.Name, item.Value + "%");
                    parameter.SqlDbType = SqlDbType.NVarChar;
                }
                else if (item.Operator == SqlOperator.Equal)
                {
                    parameter = new SqlParameter("@" + item.Name, item.Value);
                }
                else if (item.Operator == SqlOperator.NotEqual)
                {
                    parameter = new SqlParameter("@" + item.Name, item.Value);
                }
                else if (item.Operator == SqlOperator.MoreThan)
                {
                    parameter = new SqlParameter("@" + item.Name, item.Value);
                    parameter.SqlDbType = SqlDbType.NVarChar;
                }
                else if (item.Operator == SqlOperator.LessThan)
                {
                    parameter = new SqlParameter("@" + item.Name, item.Value);
                    parameter.SqlDbType = SqlDbType.NVarChar;
                }
                else if (item.Operator == SqlOperator.MoreThanOrEqual)
                {
                    parameter = new SqlParameter("@" + item.Name, item.Value);
                    parameter.SqlDbType = SqlDbType.NVarChar;
                }
                else if (item.Operator == SqlOperator.LessThanOrEqual)
                {
                    parameter = new SqlParameter("@" + item.Name, item.Value);
                    parameter.SqlDbType = SqlDbType.NVarChar;
                }
                else if (item.Operator == SqlOperator.In)
                {
                    parameter = new SqlParameter("@" + item.Name, "(" + item.Value + ")");
                    parameter.SqlDbType = SqlDbType.NVarChar;
                }
                listParameters.Add(parameter);
            }
            return listParameters;
        }


        private class Condition
        {
            public Condition(string name, object value, SqlOperator sqlOperator, SqlDbType sqlDbType)
            {
                this.Name = name;
                this.Value = value;
                this.Operator = sqlOperator;
                this.DbType = sqlDbType;
            }

            public Condition(string name, object value, SqlOperator sqlOperator)
            {
                this.Name = name;
                this.Value = value;
                this.Operator = sqlOperator;
                this.DbType = SqlDbType.NVarChar;
            }
            public string Name
            {
                get;
                set;
            }
            public object Value
            {
                get;
                set;
            }
            public SqlOperator Operator
            {
                get;
                set;
            }
            public SqlDbType DbType
            {
                get;
                set;
            }
        }
    }

    /// <summary>
    /// Sql的查询符号
    /// </summary>
    public enum SqlOperator
    {
        [Description("Like 模糊查询")]
        Like,

        [Description("Not LiKE 模糊查询")]
        NotLike,

        [Description("Like 开始匹配模糊查询，如Like 'ABC%'")]
        LikeStartAt,

        [Description("＝ 等于号")]
        Equal,

        [Description("<> (≠) 不等于号")]
        NotEqual,

        /// <summary>
        /// ＞ 大于号
        /// </summary>
        [Description("＞ 大于号")]
        MoreThan,

        [Description("＜小于号")]
        LessThan,

        [Description("≥大于或等于号 ")]
        MoreThanOrEqual,

        [Description("≤ 小于或等于号")]
        LessThanOrEqual,

        [Description("在某个字符串值中")]
        In
    }
}
