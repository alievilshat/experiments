
TRUNCATE TABLE webcategories;
INSERT INTO webcategories (categoryid,
	parentid,
	categoryname,
	categorynametranslation,
	description,
	sequence,
	largedescription,
	metakeywords,
	metadescription,
	metatitle,
	imageid,
	image2id,
	image3id,
	businessid)
SELECT c.categoryid AS categoryid,
	null AS parentid,
	c.categoryname AS categoryname,
	c.categoryname AS categorynametranslation,
	c.description AS description,
	c.sequence AS sequence,
	c.largedescription AS largedescription,
	c.metakeywords AS metakeywords,
	c.metadescription AS metadescription,
	c.metatitle AS metatitle,
	c.imageid AS imageid,
	c.image2id AS image2id,
	c.image3id AS image3id,
	cb.businessid AS businessid
FROM category c
INNER JOIN categorybusiness cb ON cb.categoryid = c.categoryid
WHERE parentid = 12 AND iswebsite;