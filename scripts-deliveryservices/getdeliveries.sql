with deliveryproduct_acc as(
	select
    	d.deliveryid,
        first(c.personid) as clientid,
        first(o.businessid) as businessid,
        first(o.deliveryaddressid) as deliveryaddressid,
        max(case when p.producttypeid = 1 then p.productid else 0 end) as deliveryproductid, -- TODO: first not-null
        sum(ordermanager_price(dd.salesprice, dd.quantity, dd.discount, dd.tax)) as totalprice,
        sum(coalesce(dd.weight, 0) * dd.quantity) as fullweight,
		array_to_string(array_agg(distinct o.shipmentremark), ',') as shipmentremark,
        array_to_string(array_agg(distinct dd.orderid), ',') as orderslist
    from delivery d
    inner join orderdetails dd on dd.deliveryid = d.deliveryid
    inner join product p on p.productid = dd.productid
    inner join orders o on o.orderid = dd.orderid
    inner join person c on c.personid = o.clientid
    group by d.deliveryid
)
SELECT
    d.deliveryid,
    b.companyname AS businessname,
    sdc.deliveryserviceiban,
    ds.name as servicename,
    03085 AS innerdeliverytype,
    dacc.totalprice,
    dacc.fullweight,
    getclientfullname(cl.firstname, cl.lastname, cl.infix) AS clientname,
    dacc.orderslist,
    500 AS insuredamount,
    CASE
      WHEN length(COALESCE(da.recipient, '')) > 0 THEN da.recipient
      ELSE COALESCE(company.companyname, '')
    END AS companyname,
    (
      SELECT count(1)
      FROM orderdetails
      WHERE deliveryid = d.deliveryid
    ) AS numberofproducts,
    (
      SELECT sum(quantity)
      FROM orderdetails
      WHERE deliveryid = d.deliveryid
    ) AS deliveryquantity,
    dacc.shipmentremark,
    da.streetaddress AS deliveryaddress,
    da.housenumber AS deliveryhousenumber,
    da.housenumberadd AS deliveryhousenumberadd,
    btrim(da.zipcode) AS deliveryzipcode,
    da.city AS deliverycity,
    cl.phonebusiness,
    cl.phonemobile,
    cl.fax,
    cl.email,
    dc.countryname,
    dcomp.externalkey as clientnr
  FROM delivery d
  		inner join deliveryproduct_acc dacc on dacc.deliveryid = d.deliveryid
        inner join company b on b.companyid = dacc.businessid
        inner join person cl ON cl.personid = dacc.clientid
        left join company ON company.companyid = cl.companyid
        inner join address da ON da.addressid = dacc.deliveryaddressid
        inner join country dc ON dc.countryid = da.countryid
        inner join product ds ON ds.productid = dacc.deliveryproductid
        left join stockdeliverycompany sdc ON sdc.stockdeliverycompanyid = ds.stockdeliverycompanyid
        inner join company dcomp ON dcomp.companyid = sdc.deliverycompanyid