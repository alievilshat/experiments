<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select distinct c.categoryid, p.varianttypeid2 from str_product p inner join str_productcategory c on c.productid = p.id where coalesce(p.varianttypeid2, 0) &gt; 0 and c.categoryid in (select id from str_category)')">
          <categoryfeature>
            <categoryid m:left="-139" m:top="204">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <featureid m:left="-193" m:top="233">fk:feature(variant-<xsl:value-of select="varianttypeid2" />)</featureid>
            <categoryfeatureid m:left="-102" m:top="74">pk:categoryfeature(variant-<xsl:value-of select="categoryid" />-<xsl:value-of select="varianttypeid2" />)</categoryfeatureid>
            <isvariant m:left="100" m:top="273">1</isvariant>
          </categoryfeature>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>