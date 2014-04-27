<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select w.businessid, cw.* from str_currencywebsite cw inner join cms_website w on w.websiteid = cw.websiteid')">
          <currencycompany>
            <currencycompanyid m:left="-84" m:top="175">
              <xsl:value-of select="id" />
            </currencycompanyid>
            <currencyid m:left="45" m:top="191">
              <xsl:value-of select="currencyid" />
            </currencyid>
            <companyid m:left="-94" m:top="220">
              <xsl:value-of select="businessid" />
            </companyid>
            <isdefault m:left="47" m:top="247">
              <xsl:value-of select="isdefault" />
            </isdefault>
          </currencycompany>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>