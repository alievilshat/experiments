update product p
set parentid = (select min(productid) from product sp where sp.producttypeid = 8 and sp.largedescription = p.largedescription)
where p.producttypeid = 8;

update product p
set parentid = NULL
where p.producttypeid = 8 and p.productid = p.parentid;

update product p
set largedescription = NULL
where p.producttypeid = 8;