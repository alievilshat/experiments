<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('with product as (select p.id from str_product p inner join str_category c on c.id = p.categoryid where not p.deleted and (p.parentid is null or p.parentid in (select id from str_product where not deleted))) select * from str_productpackage pr inner join product p1 on p1.id = pr.productid inner join product p2 on p2.id = pr.parentproductid ')">
          <productpackage>
            <parentproductid m:left="-54" m:top="128">
              <xsl:value-of select="parentproductid" />
            </parentproductid>
            <productid m:left="-53" m:top="165">
              <xsl:value-of select="productid" />
            </productid>
            <quantity m:left="-51" m:top="196">
              <xsl:value-of select="quantity" />
            </quantity>
            <sequenceid m:left="-52" m:top="226">
              <xsl:value-of select="sequenceid" />
            </sequenceid>
            <isdefault m:left="-51" m:top="257">
              <xsl:value-of select="isdefault" />
            </isdefault>
            <packageproductid m:left="-54" m:top="288">pk:productpackage(<xsl:value-of select="parentproductid" />-<xsl:value-of select="productid" />)</packageproductid>
          </productpackage>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>