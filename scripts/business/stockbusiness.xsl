<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_stockbusiness')">
          <stockbusiness>
            <stockbusinessid m:left="-74" m:top="298" xmlns:m="http://www.navitas.nl/2014/Mapper">pk:stockbusiness(<xsl:value-of select="businessid" />-<xsl:value-of select="stockid" />)</stockbusinessid>
            <businessid m:left="-23" m:top="173" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="businessid" />
            </businessid>
            <stockid m:left="-50" m:top="215" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="stockid" />
            </stockid>
            <defaultstock m:left="-64" m:top="254" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="defaultstock" />
            </defaultstock>
          </stockbusiness>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>