<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('sys_paymentgatewaytypes')">
          <company>
            <companyid m:left="-23" m:top="165">pk:company(payment-<xsl:value-of select="id" />)</companyid>
            <companyname m:left="-53" m:top="264">
              <xsl:value-of select="name" />
            </companyname>
            <companycode m:left="-76" m:top="457">
              <xsl:value-of select="id" />
            </companycode>
            <companytypeid m:left="-67" m:top="325">5</companytypeid>
          </company>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>