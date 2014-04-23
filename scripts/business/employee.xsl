<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('pm_employees')">
          <person>
            <personid m:left="-91" m:top="161">pk:person(<xsl:value-of select="id" />)</personid>
            <firstname m:left="43" m:top="221">
              <xsl:value-of select="firstname" />
            </firstname>
            <lastname m:left="42" m:top="256">
              <xsl:value-of select="lastname" />
            </lastname>
            <infix m:left="-87" m:top="240">
              <xsl:value-of select="middlename" />
            </infix>
            <gender m:left="-82" m:top="275">
              <xsl:value-of select="sex" />
            </gender>
            <email m:left="36" m:top="362">
              <xsl:value-of select="email" />
            </email>
            <dateofbirth m:left="44" m:top="449">
              <xsl:value-of select="birthdate" />
            </dateofbirth>
            <password m:left="-78" m:top="466">
              <xsl:value-of select="password" />
            </password>
            <persontypeid m:left="-12" m:top="596">1</persontypeid>
            <currencyid m:left="-58" m:top="510">1</currencyid>
          </person>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>