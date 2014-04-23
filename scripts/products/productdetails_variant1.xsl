<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select * from str_product where coalesce(varianttypeid, 0) &gt; 0 and id in (select id from str_product where not deleted and producttype = 0 and (parentid is null or parentid in (select id from str_product where not deleted and producttype = 0))) and id not in (705)')">
          <productdetails>
            <productdetailsid m:left="-107" m:top="149" xmlns:m="http://www.navitas.nl/2014/Mapper">pk:productdetails(variant-<xsl:value-of select="varianttypeid" />-<xsl:value-of select="id" />)</productdetailsid>
            <productid m:left="-104" m:top="185" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </productid>
            <featureid m:left="-102" m:top="220" xmlns:m="http://www.navitas.nl/2014/Mapper">fk:feature(variant-<xsl:value-of select="varianttypeid" />)</featureid>
            <featurevalue m:left="-100" m:top="258" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="variantvalue" />
            </featurevalue>
            <featurevalueid m:left="-98" m:top="297" xmlns:m="http://www.navitas.nl/2014/Mapper">fk:featurevalue(variant-<xsl:value-of select="varianttypeid" />-<xsl:value-of select="variantvalue" />)</featurevalueid>
          </productdetails>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>