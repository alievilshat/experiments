<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select distinct p.brandid, ps.subsidiaryid from str_product p inner join str_productsubsidiary ps on ps.productid = p.id  where not deleted and categoryid != 0 and subsidiaryid != 0 and (parentid is null or parentid in (select id from str_product where not deleted and categoryid != 0))')">
          <brandbusiness>
            <brandid m:left="-217" m:top="169">
              <xsl:value-of select="brandid" />
            </brandid>
            <iswebsite m:left="-217" m:top="234">1</iswebsite>
            <businessid m:left="-217" m:top="202">
              <xsl:value-of select="subsidiaryid" />
            </businessid>
            <brandbusinessid m:left="-510" m:top="89">pk:brandbusiness(<xsl:value-of select="subsidiaryid" />-<xsl:value-of select="brandid" />)</brandbusinessid>
          </brandbusiness>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>