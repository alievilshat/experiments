<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select distinct p.brandid, ps.subsidiaryid from str_product p inner join str_productsubsidiary ps on ps.productid = p.id  where not deleted and true and categoryid != 0 and categoryid != 1 and (parentid is null or parentid in (select id from str_product where not deleted and true and categoryid != 0 and categoryid != 1))')">
          <brandbusiness>
            <brandid m:left="-72" m:top="186">
              <xsl:value-of select="brandid" />
            </brandid>
            <iswebsite m:left="-76" m:top="296">1</iswebsite>
            <businessid m:left="-74" m:top="218">
              <xsl:value-of select="subsidiaryid" />
            </businessid>
            <brandbusinessid m:left="-220" m:top="66">pk:brandbusiness(<xsl:value-of select="subsidiaryid" />-<xsl:value-of select="brandid" />)</brandbusinessid>
          </brandbusiness>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>