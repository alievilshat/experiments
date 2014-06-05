<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:table('str_category')">
          <categorybusiness>
            <categoryid m:left="-70" m:top="298">
              <xsl:value-of select="id" />
            </categoryid>
            <iswebsite m:left="-76" m:top="296">1</iswebsite>
            <businessid m:left="-74" m:top="218">1</businessid>
            <categorybusinessid m:left="-186" m:top="132">pk:categorybusiness1-<xsl:value-of select="id" />)</categorybusinessid>
          </categorybusiness>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>