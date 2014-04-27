<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:table('sys_country')">
          <country>
            <countryid m:left="-136" m:top="66" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </countryid>
            <countryname m:left="36" m:top="82" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </countryname>
            <code m:left="-136" m:top="98" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="code" />
            </code>
            <phonecode m:left="-50" m:top="142" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="phonecode" />
            </phonecode>
            <showfirst m:left="-50" m:top="176" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="showfirst" />
            </showfirst>
            <inactive m:left="-50" m:top="210" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="recordstatus" />
            </inactive>
          </country>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>