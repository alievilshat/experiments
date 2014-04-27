<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select distinct  varianttypeid, variantvalue from str_product where coalesce(varianttypeid, 0) &gt; 0')">
          <featurevalue>
            <featurevalueid m:left="49" m:top="439" xmlns:m="http://www.navitas.nl/2014/Mapper">pk:featurevalue(variant-<xsl:value-of select="varianttypeid" />-<xsl:value-of select="variantvalue" />)</featurevalueid>
            <featureid m:left="-171" m:top="165" xmlns:m="http://www.navitas.nl/2014/Mapper">fk:feature(variant-<xsl:value-of select="varianttypeid" />)</featureid>
            <optionvalue m:left="-44" m:top="184" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="variantvalue" />
            </optionvalue>
          </featurevalue>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>