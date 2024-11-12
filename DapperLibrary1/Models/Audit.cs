using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DapperLibrary1.Models;

public class Audit
{
    // add properties to use for various data operations
}

public class AuditOperations
{
    public static bool AddAudit(Audit audit)
    {
        using SqlConnection cn = new("TODO");
        // Execute returns the number of rows affected, in this case we expect 1
        return cn.Execute(
            """
            INSERT INTO trauditmain
                (
                     docid, 
                     temporarydocno,
                     temporarydocdate,
                     referanceno,
                     referencedate,
                     permanentdocno,
                     permanentdocdate,
                     operationlog,
                     entrydate,
                     userid
                 )
            VALUES
                (
                     @docid,
                     @temporarydocno,
                     @temporarydocdate,
                     @referanceno,
                     @referencedate,
                     @permanentdocno,
                     @permanentdocdate,
                     @operationlog,
                     @entrydate,
                     @userid
                 )
            """, audit) == 1;
    }
}
