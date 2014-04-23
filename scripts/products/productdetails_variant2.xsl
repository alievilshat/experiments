<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_product where coalesce(varianttypeid2, 0) &gt; 0 and id in (select id from str_product where not deleted and producttype = 0 and (parentid is null or parentid in (select id from str_product where not deleted and producttype = 0))) and id not in (705)')">
          <productdetails>
            <productdetailsid m:left="-97" m:top="94">pk:productdetails(variant-<xsl:value-of select="varianttypeid2" />-<xsl:value-of select="id" />)</productdetailsid>
            <productid m:left="-104" m:top="185">
              <xsl:value-of select="id" />
            </productid>
            <featureid m:left="-117" m:top="262">fk:feature(variant-<xsl:value-of select="varianttypeid2" />)</featureid>
            <featurevalue m:left="-84" m:top="306">
              <xsl:value-of select="variantvalue2" />
            </featurevalue>
            <featurevalueid m:left="-93" m:top="351">fk:featurevalue(variant-<xsl:value-of select="varianttypeid2" />-<xsl:value-of select="variantvalue2" />)</featurevalueid>
          </productdetails>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>