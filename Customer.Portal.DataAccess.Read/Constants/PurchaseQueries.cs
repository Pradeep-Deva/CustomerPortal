using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Portal.DataAccess.Read.Constants
{
    internal static class PurchaseQueries
    {
        internal const string GET_CUSTOMER_DETAILS = @"
            SELECT 
            FIRSTNAME,
            LASTNAME 
                FROM 
                CUSTOMERS WITH (NOLOCK)
            WHERE 
            EMAIL=@EmailId and CUSTOMERID=@CustomerId ";


        internal const string GET_LASTORDER_BASIC_DETAILS = @"
            SELECT TOP 1
            ORD.ORDERID AS ORDERNUMBER,
            ORD.ORDERDATE  AS ORDERDATE ,
            CUST.HOUSENO+' '+CUST.STREET +', '+CUST.TOWN +', '+CUST.POSTCODE AS DELIVERYADDRESS,
            ORD.DELIVERYEXPECTED
                FROM 
                ORDERS ORD WITH (NOLOCK) JOIN 
                CUSTOMERS CUST WITH (NOLOCK) ON ORD.CUSTOMERID=CUST.CUSTOMERID
            WHERE CUST.CUSTOMERID=@CustomerId ORDER BY ORDERDATE DESC";

        internal const string GET_LASTORDER_PRODUCT_DETAILS = @"
            SELECT 
            PDT.PRODUCTNAME AS PRODUCT,
            OITMS.QUANTITY,
            OITMS.PRICE AS PRICEEACH 
                FROM ORDERS ODRS  WITH (NOLOCK) JOIN 
                ORDERITEMS OITMS  WITH (NOLOCK) ON ODRS.ORDERID=OITMS.ORDERID JOIN 
                PRODUCTS PDT  WITH (NOLOCK) ON PDT.PRODUCTID = OITMS.PRODUCTID
            WHERE ODRS.ORDERID=@OrderId";
    };
}
