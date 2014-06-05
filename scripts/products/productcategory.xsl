<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_productcategory pc inner join str_product p on p.id = pc.productid inner join str_category c on c.id = pc.categoryid inner join str_category pt on pt.id = p.categoryid where not p.deleted and (p.parentid is null or p.parentid in (select id from str_product where not deleted))')">
          <productcategory>
            <productid m:left="-137" m:top="162">
              <xsl:value-of select="productid" />
            </productid>
            <categoryid m:left="-14" m:top="180">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <productcategorid m:left="-139" m:top="197">pk:productcategory(<xsl:value-of select="productid" />-<xsl:value-of select="categoryid" />)</productcategorid>
          </productcategory>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>