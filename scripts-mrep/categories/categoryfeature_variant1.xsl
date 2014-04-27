<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select distinct c.categoryid, p.varianttypeid from str_product p inner join str_productcategory c on c.productid = p.id where coalesce(p.varianttypeid, 0) &gt; 0 and c.categoryid in (select id from str_category)')">
          <categoryfeature>
            <categoryid m:left="-139" m:top="204">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <featureid m:left="-168" m:top="300">fk:feature(variant-<xsl:value-of select="varianttypeid" />)</featureid>
            <categoryfeatureid m:left="-137" m:top="147">pk:categoryfeature(<xsl:value-of select="categoryid" />-<xsl:value-of select="varianttypeid" />)</categoryfeatureid>
            <isvariant m:left="100" m:top="273">1</isvariant>
          </categoryfeature>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>