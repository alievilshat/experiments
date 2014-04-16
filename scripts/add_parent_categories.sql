insert into categorybusiness(businessid, categoryid, iswebsite)
select distinct 3 as businessid, c.parentid as categoryid, true as iswebsite
from categorybusiness cb inner join category c on c.categoryid = cb.categoryid
where cb.businessid = 3
and c.parentid is not null and c.parentid not in (select categoryid from categorybusiness where businessid = 3)