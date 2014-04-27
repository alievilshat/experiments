<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select distinct  varianttypeid2, variantvalue2 from str_product where coalesce(varianttypeid2, 0) &gt; 0')">
          <featurevalue>
            <featurevalueid m:left="49" m:top="439" xmlns:m="http://www.navitas.nl/2014/Mapper">pk:featurevalue(variant-<xsl:value-of select="varianttypeid2" />-<xsl:value-of select="variantvalue2" />)</featurevalueid>
            <featureid m:left="-198" m:top="110" xmlns:m="http://www.navitas.nl/2014/Mapper">fk:feature(variant-<xsl:value-of select="varianttypeid2" />)</featureid>
            <optionvalue m:left="-44" m:top="184" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="variantvalue2" />
            </optionvalue>
          </featurevalue>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>