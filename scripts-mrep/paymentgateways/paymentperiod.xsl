<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select * from str_paymentperiod where languageid = 2')">
          <paymentperiod>
            <paymentperiodid m:left="-30" m:top="157">
              <xsl:value-of select="id" />
            </paymentperiodid>
            <paymentperiod m:left="-30" m:top="124">
              <xsl:value-of select="paymentperiod" />
            </paymentperiod>
          </paymentperiod>
        </xsl:for-each>
        <xsl:for-each select="mrep:query('select * from str_paymentperiod where languageid = 1')">
          <translation>
            <translationid m:left="-188" m:top="186">pk:translation(paymentperiod-<xsl:value-of select="id" />)</translationid>
            <translation m:left="-31" m:top="302">
              <xsl:value-of select="paymentperiod" />
            </translation>
            <languageid m:left="-31" m:top="337">
              <xsl:value-of select="languageid" />
            </languageid>
            <tablename m:left="-30" m:top="236">paymentperiod</tablename>
            <fieldname m:left="-31" m:top="269">paymentperiod</fieldname>
            <rowid m:left="-32" m:top="385">
              <xsl:value-of select="user:getrowid(id)" />
            </rowid>
          </translation>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="user">
   public int getrowid(int val) {
      return val - 3;
   }
</msxsl:script>
</xsl:stylesheet>