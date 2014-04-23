<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_deliverycompany')">
          <company>
            <companyid m:left="-104" m:top="161">pk:company(delivery-<xsl:value-of select="id" />)</companyid>
            <companyname m:left="19" m:top="385">
              <xsl:value-of select="companyname" />
            </companyname>
            <fax m:left="19" m:top="207">
              <xsl:value-of select="fax" />
            </fax>
            <phone m:left="-105" m:top="189">
              <xsl:value-of select="phone" />
            </phone>
            <email m:left="-107" m:top="226">
              <xsl:value-of select="email" />
            </email>
            <companytypeid m:left="-107" m:top="403">4</companytypeid>
          </company>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>