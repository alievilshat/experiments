update str_discountquantity set percentage = 30
where quantityid in (
  select dq.* from str_product p
  inner join str_discountobject ob on ob.itemid = p.id and ob.objecttypeid = 4
  inner join str_discountquantity dq on dq.groupid = ob.groupid
  where brandid = 75 and parentid is null
)

update str_productsubsidiary set discountpercentdefault = 30
where id in (
  select ps.id from str_productsubsidiary ps
  inner join str_product p on p.id = ps.productid
  where p.brandid = 75
)